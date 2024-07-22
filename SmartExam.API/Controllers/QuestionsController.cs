using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartExam.Application.DTOs.ApiResponse;
using SmartExam.Application.DTOs.Question;
using SmartExam.Application.Interfaces.Repositories;
using SmartExam.Domain.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SmartExam.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private IValidator<Question> _validator;
        private IMapper _mapper;
        private IUnitOfWork _unitOfWork;
        public QuestionsController(IValidator<Question> validator, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _validator = validator;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }


        // GET: api/<QuestionsController>
        [HttpGet("GetByModelId")]
        public async Task<IActionResult> GetByModelId(int modelId)
        {
            ApiResponse<IList<QuestionDTO>> response = new ApiResponse<IList<QuestionDTO>>();

            IList<Question> questions = await _unitOfWork.QuestionRepository.GetWhereAsync(a => a.ModelId == modelId);
            
            IList<QuestionDTO> data = [];
            if (questions.Count() > 0)
            {
                data = _mapper.Map<IList<QuestionDTO>>(questions);
            }

            response.IsSuccess = true;
            response.Data = data;

            return Ok(response);
        }

        // GET api/<QuestionsController>/5
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int id)
        {
            ApiResponse<QuestionDTO> response = new ApiResponse<QuestionDTO>();

            Question question = await _unitOfWork.QuestionRepository.GetByIdAsync(id);
            QuestionDTO? data = null;
            if (question is not null)
            {
                data = _mapper.Map<QuestionDTO>(question);
            }

            response.IsSuccess= true;
            response.Data = data;

            return Ok(response);
        }

        // POST api/<QuestionsController>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddQuestionDTO dto)
        {
            ApiResponse<AddQuestionDTO> response = new ApiResponse<AddQuestionDTO>();

            var findModelById = await _unitOfWork.ModelRepository.GetByIdAsync(dto.ModelId);
            if (findModelById is not null)
            {
                Question question = _mapper.Map<Question>(dto);

                var validationResult = await _validator.ValidateAsync(question);
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
                    await _unitOfWork.QuestionRepository.AddAsync(question);
                    await _unitOfWork.CompleteAsync();

                    response.IsSuccess = true;
                    response.Data = dto;
                    response.Message = "تم الاضافة بنجاح";

                    return Ok(response);
                }
            }
            else
            {
                response.ErrorMessages!.Add("Invalid Model Id");
                return BadRequest(response);
            }
        }

        // PUT api/<QuestionsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateQuestionDTO dto)
        {
            ApiResponse<Question> response = new ApiResponse<Question>();
             
            Question question = await _unitOfWork.QuestionRepository.GetByIdAsync(id);
            if (question is not null)
            {
                question.QuestionName = dto.QuestionName;
                question.Answer1 = dto.Answer1;
                question.Answer2 = dto.Answer2;
                question.Answer3 = dto.Answer3;
                question.Answer4 = dto.Answer4;
                question.CorrectAnswer = dto.CorrectAnswer;

                var validationResult = await _validator.ValidateAsync(question);
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
                    await _unitOfWork.QuestionRepository.UpdateAsync(id, question);
                    await _unitOfWork.CompleteAsync();

                    response.IsSuccess = true;
                    response.Message = "تم التعديل بنجاح";
                    response.Data = question;

                    return Ok(response);
                }
            }
            else
            {
                response.ErrorMessages!.Add("لا يوجد سؤال");
                return NotFound(response);
            }
        }

        // DELETE api/<QuestionsController>/5
        [HttpPost("Delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteQuestionDTO dto)
        {
            ApiResponse<string> response = new ApiResponse<string>();
             
            Question question = await _unitOfWork.QuestionRepository.GetByIdAsync(dto.Id);
            if (question is not null)
            {
                await _unitOfWork.QuestionRepository.DeleteAsync(dto.Id);
                await _unitOfWork.CompleteAsync();

                response.IsSuccess = true;
                response.Message = "تم الحذف بنجاح";

                return Ok(response);
            }
            else
            {
                response.ErrorMessages!.Add("لايوجد بيانات لحذفها");
                return BadRequest(response);
            }
        }
    }
}
