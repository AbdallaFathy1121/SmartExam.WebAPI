﻿using AutoMapper;
using Domain.Constants;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartExam.Application.DTOs.ApiResponse;
using SmartExam.Application.DTOs.Exam;
using SmartExam.Application.DTOs.ExamQuery;
using SmartExam.Application.Interfaces.Repositories;
using SmartExam.Domain.Entities;
using System.Globalization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SmartExam.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamQueriesController : ControllerBase
    {
        private IValidator<ExamQuery> _validator;
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public ExamQueriesController(IUnitOfWork unitOfWork, IValidator<ExamQuery> validator, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
            _mapper = mapper;
        }

        [Authorize(Roles = Roles.Admin)]
        // GET: api/<ExamQueriesController>
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            ApiResponse<IReadOnlyList<ExamQueryDTO>> response = new ApiResponse<IReadOnlyList<ExamQueryDTO>>();

            IReadOnlyList<ExamQuery> examQueries = await _unitOfWork.ExamQueryRepository.GetAllAsync();

            // Mapper
            IReadOnlyList<ExamQueryDTO> data = _mapper.Map<IReadOnlyList<ExamQueryDTO>>(examQueries);

            response.IsSuccess = true;
            response.Data = data;
            return Ok(response);
        }

        // GET api/<ExamQueriesController>/GetByExamId/5
        [HttpGet("GetByExamId/{examId}")]
        public async Task<IActionResult> GetByExamId(int examId)
        {
            ApiResponse<IList<ExamQueryDTO>> response = new ApiResponse<IList<ExamQueryDTO>>();

            IList<ExamQuery> examQueries = await _unitOfWork.ExamQueryRepository.GetWhereAsync(a => a.ExamId == examId);

            List<int> res = await _unitOfWork.ExamQueryRepository.GetRandomQuestionIdByQuery(examQueries);

            // Mapper
            IList<ExamQueryDTO> data = _mapper.Map<IList<ExamQueryDTO>>(examQueries);

            response.IsSuccess = true;
            response.Data = data;
            return Ok(response);
        }

        // GET api/<ExamQueriesController>/GetByExamId/5
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            ApiResponse<ExamQueryDTO> response = new ApiResponse<ExamQueryDTO>();

            ExamQuery examQuery = await _unitOfWork.ExamQueryRepository.GetByIdAsync(id);
            if (examQuery is not null)
            {
                // Mapper
                ExamQueryDTO data = _mapper.Map<ExamQueryDTO>(examQuery);

                response.IsSuccess = true;
                response.Data = data;
                return Ok(response);
            }
            else
            {
                response.ErrorMessages!.Add("Not Found ExamQuery by this Id");
                return NotFound(response);
            }
        }

        // GET api/<ExamQueriesController>/GetByExamId/5
        [HttpGet("GetListOfQuestionId/{examId}")]
        public async Task<IActionResult> GetListOfQuestionId(int examId)
        {
            ApiResponse<List<int>> response = new ApiResponse<List<int>>();

            IList<ExamQuery> examQueries = await _unitOfWork.ExamQueryRepository.GetWhereAsync(a => a.ExamId == examId);

            if (examQueries.Count() <= 0)
            {
                response.ErrorMessages!.Add("Not found Exam Queries");
                return NotFound(response);
            }
            else
            {
                // Return List Of QuestionId 
                List<int> res = await _unitOfWork.ExamQueryRepository.GetRandomQuestionIdByQuery(examQueries);

                response.IsSuccess = true;
                response.Data = res;
                return Ok(response);
            }
        }

        [Authorize]
        // POST api/<ExamQueriesController>
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] AddExamQueryDTO dto)
        {
            ApiResponse<AddExamQueryDTO> response = new ApiResponse<AddExamQueryDTO>();

            Model findModel = await _unitOfWork.ModelRepository.GetByIdAsync(dto.ModelId);
            if (findModel is null)
            {
                response.ErrorMessages!.Add("Invalid Model Id");
                return NotFound(response);
            }

            Exam findExamId = await _unitOfWork.ExamRepository.GetByIdAsync(dto.ExamId);
            if (findExamId is not null)
            {
                ExamQuery examQuery = _mapper.Map<ExamQuery>(dto);

                var validationResult = await _validator.ValidateAsync(examQuery);
                if (!validationResult.IsValid)
                {
                    foreach (var error in validationResult.Errors)
                    {
                        response.ErrorMessages!.Add(error.ErrorMessage);
                    }
                    return BadRequest(response);
                }
                else
                {
                    await _unitOfWork.ExamQueryRepository.AddAsync(examQuery);
                    await _unitOfWork.CompleteAsync();

                    response.IsSuccess = true;
                    response.Data = dto;
                    response.Message = "تم الاضافة بنجاح";

                    return Ok(response);
                }
            }
            else
            {
                response.ErrorMessages!.Add("Invalid Exam Id");
                return NotFound(response);
            }
        }

        [Authorize]
        // PUT api/<ExamQueriesController>/Update/5
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateExamQueryDTO dto)
        {
            ApiResponse<ExamQuery> response = new ApiResponse<ExamQuery>();

            Model findModel = await _unitOfWork.ModelRepository.GetByIdAsync(dto.ModelId);
            if (findModel is null)
            {
                response.ErrorMessages!.Add("Invalid Model Id");
                return NotFound(response);
            }

            ExamQuery examQuery = await _unitOfWork.ExamQueryRepository.GetByIdAsync(id);
            if (examQuery is not null)
            {
                examQuery.ModelId = dto.ModelId;
                examQuery.QuestionNumbers = dto.QuestionNumbers;

                var validationResult = await _validator.ValidateAsync(examQuery);
                if (!validationResult.IsValid)
                {
                    foreach (var error in validationResult.Errors)
                    {
                        response.ErrorMessages!.Add(error.ErrorMessage);
                    }
                    return BadRequest(response);
                }
                else
                {
                    await _unitOfWork.ExamQueryRepository.UpdateAsync(id, examQuery);
                    await _unitOfWork.CompleteAsync();

                    response.IsSuccess = true;
                    response.Data = examQuery;
                    response.Message = "تم التعديل بنجاح";

                    return Ok(response);
                }
            }
            else
            {
                response.ErrorMessages!.Add("Not Found ExamQuery with this Id");
                return NotFound(response);
            }
        }

        [Authorize]
        // DELETE api/<ExamQueriesController>/Delete
        [HttpPost("Delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteExamQueryDTO dto)
        {
            ApiResponse<string> response = new ApiResponse<string>();

            ExamQuery examQuery = await _unitOfWork.ExamQueryRepository.GetByIdAsync(dto.Id);
            if (examQuery is not null)
            {
                await _unitOfWork.ExamQueryRepository.DeleteAsync(dto.Id);
                await _unitOfWork.CompleteAsync();

                response.IsSuccess = true;
                response.Message = "تم الحذف بنجاح";

                return Ok(response);
            }
            else
            {
                response.ErrorMessages!.Add("Invalid Id");
                return BadRequest(response);
            }
        }
    }
}
