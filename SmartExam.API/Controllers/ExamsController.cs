using AutoMapper;
using Domain.Constants;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartExam.Application.DTOs.ApiResponse;
using SmartExam.Application.DTOs.Exam;
using SmartExam.Application.Interfaces.Repositories;
using SmartExam.Domain.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SmartExam.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ExamsController : ControllerBase
    {
        private IValidator<Exam> _validator;
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public ExamsController(IUnitOfWork unitOfWork, IValidator<Exam> validator, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
            _mapper = mapper;
        }


        // GET: api/<ExamsController>
        [Authorize(Roles = Roles.Admin)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            ApiResponse<IReadOnlyList<ExamDTO>> response = new ApiResponse<IReadOnlyList<ExamDTO>>();

            IReadOnlyList<Exam> exams = await _unitOfWork.ExamRepository.GetAllAsync();

            IReadOnlyList<ExamDTO> data = [];
            if (exams.Count() > 0)
            {
                data = _mapper.Map<IReadOnlyList<ExamDTO>>(exams);
            }

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

            IList<ExamDTO> data = [];
            if (exams.Count() > 0)
            {
                data = _mapper.Map<IList<ExamDTO>>(exams);
            }

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

        // POST api/<ExamsController>
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromForm] AddExamDTO dto)
        {
            ApiResponse<AddExamDTO> response = new ApiResponse<AddExamDTO>();

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

                        response.IsSuccess = true;
                        response.Data = dto;
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

        // PUT api/<ExamsController>/5
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] UpdateExamDTO dto)
        {
            ApiResponse<Exam> response = new ApiResponse<Exam>();

            Exam exam = await _unitOfWork.ExamRepository.GetByIdAsync(id);
            if (exam is not null)
            {
                exam.Name = dto.Name;
                exam.StartDate = dto.StartDate;
                exam.StartTime = dto.StartTime;
                exam.DurationTime = dto.DurrationTime;
                exam.Status = dto.Status;

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
