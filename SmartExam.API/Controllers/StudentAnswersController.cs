using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SmartExam.Application.DTOs.ApiResponse;
using SmartExam.Application.DTOs.StudentAnswer;
using SmartExam.Application.DTOs.StudentExamQuestion;
using SmartExam.Application.Interfaces.Repositories;
using SmartExam.Domain.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SmartExam.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentAnswersController : ControllerBase
    {
        private IValidator<StudentAnswer> _validator;
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public StudentAnswersController(IUnitOfWork unitOfWork, IMapper mapper, IValidator<StudentAnswer> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
            _mapper = mapper;
        }


        // GET: api/<StudentAnswersController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // POST api/<StudentAnswersController>/ Add
        [HttpPost("AddList")]
        public async Task<IActionResult> Add([FromBody] IList<AddStudentAnswerDTO> dtos)
        {
            ApiResponse<IList<StudentAnswerDTO>> response = new ApiResponse<IList<StudentAnswerDTO>>();

            StudentExam findStudnetExamById = await _unitOfWork.StudentExamRepository.GetByIdAsync(dtos[0].StudentExamId);
            if (findStudnetExamById is null)
            {
                response.ErrorMessages!.Add("Invalid Student Exam Id");
                return BadRequest(response);
            }

            StudentAnswer findStudentAnswerByStudentExamId = await _unitOfWork.StudentAnswerRepository.GetFirstAsync(a => a.StudentExamId == dtos[0].StudentExamId);
            if (findStudentAnswerByStudentExamId is not null)
            {
                response.ErrorMessages!.Add("Student Exam Id is Already existe");
                return BadRequest(response);
            }

            IList<StudentAnswer> studentAnswers = new List<StudentAnswer>();

            foreach (var item in dtos)
            {
                Question getQuestionById = await _unitOfWork.QuestionRepository.GetByIdAsync(item.QuestionId);
                if (getQuestionById is null)
                {
                    response.ErrorMessages!.Add("Invalid Question Id");
                    return BadRequest(response);
                }

                bool isCorrect = false;
                if (getQuestionById.CorrectAnswer == item.SelectedAnswer)
                {
                    isCorrect = true;
                }

                StudentAnswer studentAnswer = new StudentAnswer { 
                    StudentExamId =  item.StudentExamId, 
                    QuestionId = item.QuestionId, 
                    SelectedAnswer = item.SelectedAnswer,
                    IsCorrect = isCorrect
                };

                studentAnswers.Add(studentAnswer);
            }

            // Save In Database
            await _unitOfWork.StudentAnswerRepository.AddRangeAsync(studentAnswers);
            await _unitOfWork.CompleteAsync();

            // AutoMapper
            IList<StudentAnswerDTO> data = _mapper.Map<IList<StudentAnswerDTO>>(studentAnswers);

            response.IsSuccess = true;
            response.Data = data;
            return Ok(response);
        }
    }
}
