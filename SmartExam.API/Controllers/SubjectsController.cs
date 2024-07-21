using Application.Interfaces.Services;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartExam.Application.DTOs.ApiResponse;
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
        //[HttpGet]
        //public async Task<IActionResult> GetAll()
        //{
        //    ApiResponse<List<Subject>> apiResponse = new ApiResponse<List<Subject>>();
        //    try
        //    {

        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }
        //}

        // GET api/<SubjectsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<SubjectsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddSubjectDTO dto)
        {
            ApiResponse<string> response = new ApiResponse<string>();
            try
            {
                var findUser = await _userService.GetUserByIdAsync(dto.UserId);
                if (findUser is not null) 
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
            catch (Exception ex)
            {
                response.ErrorMessages!.Add(ex.Message);
                return BadRequest(response);
            }
        }

        // PUT api/<SubjectsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<SubjectsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
