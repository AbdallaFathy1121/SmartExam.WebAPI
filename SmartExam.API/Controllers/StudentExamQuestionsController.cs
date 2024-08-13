using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SmartExam.Application.DTOs.ApiResponse;
using SmartExam.Application.DTOs.Chapter;
using SmartExam.Application.DTOs.StudentExamQuestion;
using SmartExam.Application.Interfaces.Repositories;
using SmartExam.Domain.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SmartExam.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentExamQuestionsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public StudentExamQuestionsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        // POST api/<StudentExamQuestionsController>
        [HttpPost("AddQuestions")]
        public async Task<IActionResult> AddQuestions([FromBody] AddStudentExamQuestionDTO dto)
        {
            ApiResponse<IList<StudentExamQuestionDTO>> response = new ApiResponse<IList<StudentExamQuestionDTO>>();

            StudentExam findStudentExam = await _unitOfWork.StudentExamRepository.GetByIdAsync(dto.StudentExamId);
            if (findStudentExam is null)
            {
                response.ErrorMessages!.Add("Invalid Student Exam Id");
                return NotFound(response);
            }

            StudentExamQuestion findStudentExamQueston = await _unitOfWork.StudentExamQuestionRepository.GetFirstAsync(a => a.StudentExamId == dto.StudentExamId);
            if (findStudentExamQueston is not null)
            {
                response.ErrorMessages!.Add("Student Exam Id already exists");
                return BadRequest(response);
            }

            // Get List Of Questions By Exam Id and Execute Exam Query
            IList<ExamQuery> examQueries = await _unitOfWork.ExamQueryRepository.GetWhereAsync(a => a.ExamId == findStudentExam.ExamId);
            List<int> listOfQuestionId = await _unitOfWork.ExamQueryRepository.GetRandomQuestionIdByQuery(examQueries);

            // Set All QuestionId into StudentExamQuestion
            IList<StudentExamQuestion> studentExamQuestions = new List<StudentExamQuestion>();
            foreach (var questionId in listOfQuestionId)
            {
                StudentExamQuestion studentExamQuestion = new StudentExamQuestion { QuestionId = questionId, StudentExamId = dto.StudentExamId };
                studentExamQuestions.Add(studentExamQuestion);
            }

            // Save In Database
            await _unitOfWork.StudentExamQuestionRepository.AddRangeAsync(studentExamQuestions);
            await _unitOfWork.CompleteAsync();

            // AutoMapper
            IList<StudentExamQuestionDTO> data = _mapper.Map<IList<StudentExamQuestionDTO>>(studentExamQuestions);

            response.IsSuccess = true;
            response.Data = data;
            return Ok(response);
        }
    }
}
