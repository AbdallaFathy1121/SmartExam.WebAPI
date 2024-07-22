
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartExam.Application.DTOs.ApiResponse;
using SmartExam.Application.DTOs.Chapter;
using SmartExam.Application.Interfaces.Repositories;
using SmartExam.Domain.Entities;
using System;
using System.Collections.Generic;

namespace SmartExam.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ChapterController : ControllerBase
    {
        private IValidator<Chapter> _validator;
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public ChapterController(IValidator<Chapter> validator, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _validator = validator;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: api/<ChapterController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            ApiResponse<IReadOnlyList<ChapterDTO>> response = new ApiResponse<IReadOnlyList<ChapterDTO>>();

            IReadOnlyList<Chapter> result = await _unitOfWork.ChapterRepository.GetAllAsync();
            IReadOnlyList<ChapterDTO>  dto = _mapper.Map<IReadOnlyList<ChapterDTO>>(result);

            response.IsSuccess = true;
            response.Data = dto;

            return Ok(response);
        }

        // GET api/<ChapterController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            ApiResponse<ChapterDTO> response = new ApiResponse<ChapterDTO>();

            Chapter result = await _unitOfWork.ChapterRepository.GetByIdAsync(id, []);
            ChapterDTO dto = _mapper.Map<ChapterDTO>(result);

            response.IsSuccess = true;
            response.Data = dto;

            return Ok(response);
        }

        // POST api/<ChapterController>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddChapterDTO dto)
        {
            ApiResponse<AddChapterDTO> response = new ApiResponse<AddChapterDTO>();
            
            var findSubjectById = await _unitOfWork.SubjectRepository.GetByIdAsync(dto.SubjectId);
            if (findSubjectById is null)
            {
                response.ErrorMessages!.Add("Invalid SubjectId");
                return BadRequest(response);
            }

            var exists = await _unitOfWork.ChapterRepository.GetWhereAsync(a => a.Name == dto.Name && a.SubjectId == dto.SubjectId);
            if (exists.Count() > 0)
            {
                response.ErrorMessages!.Add("هذا الاسم موجود بالفعل");
                return BadRequest(response);
            }
            else
            {
                Chapter chapter = _mapper.Map<Chapter>(dto);

                var validationResult = await _validator.ValidateAsync(chapter);
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
                    await _unitOfWork.ChapterRepository.AddAsync(chapter);
                    await _unitOfWork.CompleteAsync();

                    response.IsSuccess = true;
                    response.Data = dto;
                    response.Message = "تم الاضافة بنجاح";

                    return Ok(response);
                }
            }

        }

        // PUT api/<ChapterController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateChapterDTO dto)
        {
            ApiResponse<Chapter> response = new ApiResponse<Chapter>();

            Chapter chapter = await _unitOfWork.ChapterRepository.GetByIdAsync(id, []);
            if (chapter is null)
            {
                response.ErrorMessages!.Add("لايوجد فصل");
                return NotFound(response);
            }
            else
            {
                chapter.Name = dto.Name;

                var validationResult = await _validator.ValidateAsync(chapter);
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
                    await _unitOfWork.ChapterRepository.UpdateAsync(id, chapter);
                    await _unitOfWork.CompleteAsync();

                    response.IsSuccess = true;
                    response.Message = "تم التعديل بنجاح";
                    response.Data = chapter;

                    return Ok(response);
                }
            }
        }

        // DELETE api/<ChapterController>
        [HttpPost("Delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteChapterDTO dto)
        {
            ApiResponse<string> response = new ApiResponse<string>();
             
            var result = await _unitOfWork.ChapterRepository.GetByIdAsync(dto.Id, []);
            if (result is null)
            {
                response.ErrorMessages!.Add("لايوجد بيانات لحذفها");
                return BadRequest(response);
            }
            else
            {
                await _unitOfWork.ChapterRepository.DeleteAsync(dto.Id);
                await _unitOfWork.CompleteAsync();

                response.IsSuccess = true;
                response.Message = "تم الحذف بنجاح";

                return Ok(response);
            }
        }
    }
}
