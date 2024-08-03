using AutoMapper;
using Domain.Constants;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartExam.Application.DTOs.ApiResponse;
using SmartExam.Application.DTOs.StudentExam;
using SmartExam.Application.Interfaces.Repositories;
using SmartExam.Domain.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SmartExam.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentExamsController : ControllerBase
    {
        private IValidator<StudentExam> _validator;
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public StudentExamsController(IUnitOfWork unitOfWork, IValidator<StudentExam> validator, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
            _mapper = mapper;
        }


        [Authorize(Roles = Roles.Admin)]
        // GET: api/<StudentExamsController>
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            ApiResponse<IReadOnlyList<StudentExamDTO>> response = new ApiResponse<IReadOnlyList<StudentExamDTO>>();

            IReadOnlyList<StudentExam> studentExams = await _unitOfWork.StudentExamRepository.GetAllAsync();
            // Mapper
            IReadOnlyList<StudentExamDTO> data = _mapper.Map<IReadOnlyList<StudentExamDTO>>(studentExams);

            response.IsSuccess = true;
            response.Data = data;
            return Ok(response);
        }

        // GET api/<StudentExamsController>/5
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            ApiResponse<StudentExamDTO> response = new ApiResponse<StudentExamDTO>();

            var studentExam = await _unitOfWork.StudentExamRepository.GetByIdAsync(id);
            if (studentExam is not null)
            {
                // Mapper
                StudentExamDTO data = _mapper.Map<StudentExamDTO>(studentExam);

                response.IsSuccess = true;
                response.Data = data;
                return Ok(response);
            }
            else
            {
                response.ErrorMessages!.Add("Not found Student Exam with this Id");
                return NotFound(response);
            }
        }

        // POST api/<StudentExamsController>
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] AddStudentExamDTO dto)
        {
            ApiResponse<AddStudentExamDTO> response = new ApiResponse<AddStudentExamDTO>();

            IList<StudentExam> findStudentExam = await _unitOfWork.StudentExamRepository.GetWhereAsync(a => a.ExamId == dto.ExamId && a.Email == dto.Email);

            if (findStudentExam.Count() == 0)
            {
                StudentExam studentExam = _mapper.Map<StudentExam>(dto);
                
                var validationResult = await _validator.ValidateAsync(studentExam);
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
                    await _unitOfWork.StudentExamRepository.AddAsync(studentExam);
                    await _unitOfWork.CompleteAsync();

                    response.IsSuccess = true;
                    response.Data = dto;
                    response.Message = "تم الاضافة بنجاح";

                    return Ok(response);
                }
            }
            else
            {
                response.ErrorMessages!.Add("هذا البريد موجود بالفعل");
                return BadRequest(response);
            }
        }
    }
}
