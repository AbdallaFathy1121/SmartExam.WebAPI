using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartExam.Application.DTOs.ApiResponse;
using SmartExam.Application.Interfaces.Repositories;
using SmartExam.Domain.Entities;

namespace SmartExam.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamResultsController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;
        public ExamResultsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add ([FromBody] int studentExamId)
        {
            ApiResponse<ExamResults> response = new ApiResponse<ExamResults>();

            StudentExam getStudentExamById = await _unitOfWork.StudentExamRepository.GetByIdAsync(studentExamId);
            if (getStudentExamById is null)
            {
                response.ErrorMessages!.Add("Invalid Student Exam Id");
                return BadRequest(response);
            }

            IList<StudentAnswer> getStudentAnswersByStudentExamId = await _unitOfWork.StudentAnswerRepository.GetWhereAsync(a => a.StudentExamId == studentExamId);

            ExamResults examResults = new ExamResults
            {
                StudentExamID = studentExamId,
                TotalScore = getStudentAnswersByStudentExamId.Count(),
                StudentDegree = getStudentAnswersByStudentExamId.Count(a => a.IsCorrect),
                Percent = 0,
                Status = false
            };

            examResults.Percent = ((float)examResults.StudentDegree / (float)examResults.TotalScore) * 100;
            if (examResults.StudentDegree >= examResults.TotalScore / 2)
            {
                examResults.Status = true;
            }

            await _unitOfWork.ExamResultRepository.AddAsync(examResults);
            await _unitOfWork.CompleteAsync();

            response.IsSuccess = true;
            response.Data = examResults;
            return Ok(response);
        }
    }
}
