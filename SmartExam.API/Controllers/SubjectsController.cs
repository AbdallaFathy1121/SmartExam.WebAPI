using Application.Interfaces.Services;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartExam.Application.DTOs.ApiResponse;
using SmartExam.Application.DTOs.Chapter;
using SmartExam.Application.DTOs.Subject;
using SmartExam.Application.Interfaces.Repositories;
using SmartExam.Domain.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SmartExam.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;
        private IValidator<Subject> _validator;
        private IMapper _mapper;
        private IUserService _userService;
        public SubjectsController(IUnitOfWork unitOfWork, IValidator<Subject> validator, IMapper mapper, IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
            _mapper = mapper;
            _userService = userService;
        }


        // GET: api/<SubjectsController>
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            ApiResponse<IList<SubjectDTO>> response = new ApiResponse<IList<SubjectDTO>>();

            IReadOnlyList<Subject> subjects = await _unitOfWork.SubjectRepository.GetAllAsync();
            IList<SubjectDTO> data = _mapper.Map<IList<SubjectDTO>>(subjects);

            response.IsSuccess = true;
            response.Data = data;

            return Ok(response);
        }

        // GET api/<SubjectsController>/5
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            ApiResponse<SubjectDTO> response = new ApiResponse<SubjectDTO>();
            
            Subject subject = await _unitOfWork.SubjectRepository.GetByIdAsync(id);
            SubjectDTO data = _mapper.Map<SubjectDTO>(subject);

            response.IsSuccess = true;
            response.Data = data;

            return Ok(response);
        }

        // POST api/<SubjectsController>
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] AddSubjectDTO dto)
        {
            ApiResponse<string> response = new ApiResponse<string>();

            var findUser = await _userService.GetUserByIdAsync(dto.UserId);
            if (findUser.Data is not null) 
            {
                Subject subject = _mapper.Map<Subject>(dto);
                var validationResult = await _validator.ValidateAsync(subject);
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
                    await _unitOfWork.SubjectRepository.AddAsync(subject);
                    await _unitOfWork.CompleteAsync();

                    response.IsSuccess = true;
                    response.Message = "تم الاضافة بنجاح";

                    return Ok(response);
                }
            }
            else
            {
                response.ErrorMessages!.Add("برجاء ادخال بيانات صحيحة");
                return BadRequest(response);
            }
        }

        // PUT api/<SubjectsController>/5
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateSubjectDTO dto)
        {
            ApiResponse<Subject> response = new ApiResponse<Subject>();

            Subject subject = await _unitOfWork.SubjectRepository.GetByIdAsync(id);
            if (subject is null || subject.UserId != dto.UserId)
            {
                response.ErrorMessages!.Add("لايوجد مادة");
                return NotFound(response);
            }
            else
            {
                subject.Name = dto.Name;

                var validationResult = await _validator.ValidateAsync(subject);
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
                    await _unitOfWork.SubjectRepository.UpdateAsync(id, subject);
                    await _unitOfWork.CompleteAsync();

                    response.IsSuccess = true;
                    response.Message = "تم التعديل بنجاح";
                    response.Data = subject;

                    return Ok(response);
                }
            }

        }

        // DELETE api/<SubjectsController>/5
        [HttpPost("Delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteSubjectDTO dto)
        {
            ApiResponse<string> response = new ApiResponse<string>();

            var result = await _unitOfWork.SubjectRepository.GetByIdAsync(dto.Id);
            if (result is null)
            {
                response.ErrorMessages!.Add("لايوجد بيانات لحذفها");
                return BadRequest(response);
            }
            else
            {
                await _unitOfWork.SubjectRepository.DeleteAsync(dto.Id);
                await _unitOfWork.CompleteAsync();

                response.IsSuccess = true;
                response.Message = "تم الحذف بنجاح";

                return Ok(response);
            }
        }
    }
}
