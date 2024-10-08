﻿using Application.Interfaces.Services;
using AutoMapper;
using Domain.Constants;
using FluentValidation;
using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SmartExam.Application.DTOs.ApiResponse;
using SmartExam.Application.DTOs.Exam;
using SmartExam.Application.Interfaces.Repositories;
using SmartExam.Application.Interfaces.Services;
using SmartExam.Domain.Entities;
using System.Globalization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SmartExam.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamsController : ControllerBase
    {
        private IValidator<Exam> _validator;
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private IChangeExamStatus _changeExamStatus;
        private IUserService _userService;
        public ExamsController(IUnitOfWork unitOfWork, 
            IValidator<Exam> validator, 
            IMapper mapper, 
            IChangeExamStatus changeExamStatus,
            IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
            _mapper = mapper;
            _changeExamStatus = changeExamStatus;
            _userService = userService;
        }


        // GET: api/<ExamsController>
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            ApiResponse<IReadOnlyList<ExamDTO>> response = new ApiResponse<IReadOnlyList<ExamDTO>>();

            IReadOnlyList<Exam> exams = await _unitOfWork.ExamRepository.GetAllAsync();

            IReadOnlyList<ExamDTO> data = _mapper.Map<IReadOnlyList<ExamDTO>>(exams);

            response.IsSuccess = true;
            response.Data = data;

            return Ok(response);
        }

        // GET api/<ExamsController>/UserId
        [HttpGet("GetByUserId/{userId}")]
        public async Task<IActionResult> GetByUserId(string userId)
        {
            ApiResponse<IList<ExamDTO>> response = new ApiResponse<IList<ExamDTO>>();

            IList<Exam> exams = await _unitOfWork.ExamRepository.GetWhereAsync(a => a.UserId == userId);

            IList<ExamDTO> data = _mapper.Map<IList<ExamDTO>>(exams);

            response.IsSuccess = true;
            response.Data = data;

            return Ok(response);
        }

        // GET api/<ExamsController>/5
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            ApiResponse<ExamDTO> response = new ApiResponse<ExamDTO>();

            Exam exam = await _unitOfWork.ExamRepository.GetByIdAsync(id);

            if (exam is not null)
            {
                ExamDTO data = _mapper.Map<ExamDTO>(exam);
                
                response.IsSuccess = true;
                response.Data = data;
                return Ok(response);
            }
            else
            {
                response.ErrorMessages!.Add("لا يوجد امتحان");
                return NotFound(response);
            }
        }

        [Authorize]
        // POST api/<ExamsController>
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] AddExamDTO dto)
        {
            ApiResponse<object> response = new ApiResponse<object>();

            var findUser = await _userService.GetUserByIdAsync(dto.userId);
            if (findUser.Data is null)
            {
                response.ErrorMessages!.Add("Invalid User Id");
                return NotFound(response);
            }
          
            IList<Exam> findExamByName = await _unitOfWork.ExamRepository.GetWhereAsync(a => a.Name == dto.Name && a.UserId == dto.userId);
            if (findExamByName.Count() <= 0)
            {
                Subject findSubjectById = await _unitOfWork.SubjectRepository.GetByIdAsync(dto.subjectId);
                if (findSubjectById is not null)
                {
                    Exam exam = _mapper.Map<Exam>(dto);

                    var validationResult = await _validator.ValidateAsync(exam);
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
                        await _unitOfWork.ExamRepository.AddAsync(exam);
                        await _unitOfWork.CompleteAsync();

                        // Combine DateOnly and TimeOnly into DateTime
                        DateTime startDateTime = exam.StartDate.ToDateTime(exam.StartTime);

                        // Get current DateTime
                        DateTime currentDateTime = DateTime.Now;

                        // Calculate the time difference
                        TimeSpan timeDifference = startDateTime - currentDateTime;

                        // Convert the time difference to minutes
                        double minutesStartExam = (double)timeDifference.TotalMinutes;

                        // Calculate End Exam by Minutes
                        double minutesEndExam = minutesStartExam + dto.DurrationTime;

                        // Background Task to set ExamStatus => True When Exam Start Date and Time
                        BackgroundJob.Schedule(() => _changeExamStatus.ChangeExamStatu(exam, true), TimeSpan.FromMinutes(minutesStartExam));

                        // Background Task to set ExamStatus => False When Exam End Date and Time
                        BackgroundJob.Schedule(() => _changeExamStatus.ChangeExamStatu(exam, false), TimeSpan.FromMinutes(minutesEndExam));

                        response.IsSuccess = true;
                        response.Data = new
                        {
                            id = exam.Id,
                            name = exam.Name, 
                            status = exam.Status,
                        };
                        response.Message = "تم الاضافة بنجاح";

                        return Ok(response);
                    }
                } 
                else
                {
                    response.ErrorMessages!.Add("Invalid Subject Id");
                    return NotFound(response);
                }
            }
            else
            {
                response.ErrorMessages!.Add("هذا الاسم موجود بالفعل");
                return NotFound(response);
            }
        }

        [Authorize]
        // PUT api/<ExamsController>/5
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateExamDTO dto)
        {
            ApiResponse<Exam> response = new ApiResponse<Exam>();

            Exam exam = await _unitOfWork.ExamRepository.GetByIdAsync(id);
            if (exam is not null)
            {
                // Assuming the string is in the format "yyyy-MM-dd"
                if (DateOnly.TryParseExact(dto.StartDate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
                {
                    exam.StartDate = date;
                }

                // Assuming the string is in the format "HH:mm"
                if (TimeOnly.TryParseExact(dto.StartTime, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out var time))
                {
                    exam.StartTime = time;
                }

                exam.Name = dto.Name;
                exam.DurrationTime = dto.DurrationTime;

                var validationResult = await _validator.ValidateAsync(exam);
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
                    await _unitOfWork.ExamRepository.UpdateAsync(id, exam);
                    await _unitOfWork.CompleteAsync();

                    response.IsSuccess = true;
                    response.Data = exam;
                    response.Message = "تم التعديل بنجاح";

                    return Ok(response);
                }
            }
            else
            {
                response.ErrorMessages!.Add("Not Found Exam with this Id");
                return NotFound(response);
            }
        }

        [Authorize]
        // DELETE api/<ExamsController>/5
        [HttpPost("Delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteExamDTO dto)
        {
            ApiResponse<string> response = new ApiResponse<string>();

            Exam exam = await _unitOfWork.ExamRepository.GetByIdAsync(dto.Id);
            if (exam is not null)
            {
                await _unitOfWork.ExamRepository.DeleteAsync(dto.Id);
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
