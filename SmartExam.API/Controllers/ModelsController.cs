using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartExam.Application.DTOs.ApiResponse;
using SmartExam.Application.DTOs.Model;
using SmartExam.Application.Interfaces.Repositories;
using SmartExam.Domain.Entities;

namespace SmartExam.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ModelsController : ControllerBase
    {
        private IValidator<Model> _validator;
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public ModelsController(IValidator<Model> validator, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _validator = validator;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: api/<ModelsController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            ApiResponse<IReadOnlyList<ExamQueryDTO>> response = new ApiResponse<IReadOnlyList<ExamQueryDTO>>();

            IReadOnlyList<Model> result = await _unitOfWork.ModelRepository.GetAllAsync();
            IReadOnlyList<ExamQueryDTO> dto = _mapper.Map<IReadOnlyList<ExamQueryDTO>>(result);

            response.IsSuccess = true;
            response.Data = dto;

            return Ok(response);
        }

        // GET api/<ModelsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            ApiResponse<ExamQueryDTO> response = new ApiResponse<ExamQueryDTO>();

            Model result = await _unitOfWork.ModelRepository.GetByIdAsync(id, []);
            ExamQueryDTO dto = _mapper.Map<ExamQueryDTO>(result);

            response.IsSuccess = true;
            response.Data = dto;

            return Ok(response);
        }

        // POST api/<ModelsController>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddModelDTO dto)
        {
            ApiResponse<AddModelDTO> response = new ApiResponse<AddModelDTO>();

            var findChapterById = await _unitOfWork.ChapterRepository.GetByIdAsync(dto.ChapterId);
            if (findChapterById is null)
            {
                response.ErrorMessages!.Add("Invalid Chapter Id");
                return BadRequest(response);
            }

            var exists = await _unitOfWork.ModelRepository.GetWhereAsync(a => a.Name == dto.Name && a.ChapterId == dto.ChapterId);
            if (exists.Count() > 0)
            {
                response.ErrorMessages!.Add("هذا الاسم موجود بالفعل");
                return BadRequest(response);
            }
            else
            {
                Model model = _mapper.Map<Model>(dto);

                var validationResult = await _validator.ValidateAsync(model);
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
                    await _unitOfWork.ModelRepository.AddAsync(model);
                    await _unitOfWork.CompleteAsync();

                    response.IsSuccess = true;
                    response.Data = dto;
                    response.Message = "تم الاضافة بنجاح";

                    return Ok(response);
                }
            }
        }

        // PUT api/<ModelsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateExamQueryDTO dto)
        {
            ApiResponse<Model> response = new ApiResponse<Model>();

            Model model = await _unitOfWork.ModelRepository.GetByIdAsync(id, []);
            if (model is null)
            {
                response.ErrorMessages!.Add("لايوجد هذا النموذج");
                return NotFound(response);
            }
            else
            {
                model.Name = dto.Name;

                var validationResult = await _validator.ValidateAsync(model);
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
                    await _unitOfWork.ModelRepository.UpdateAsync(id, model);
                    await _unitOfWork.CompleteAsync();

                    response.IsSuccess = true;
                    response.Message = "تم التعديل بنجاح";
                    response.Data = model;

                    return Ok(response);
                }
            }
        }

        // DELETE api/<ModelsController>
        [HttpPost("Delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteExamQueryDTO dto)
        {
            ApiResponse<string> response = new ApiResponse<string>();

            Model model = await _unitOfWork.ModelRepository.GetByIdAsync(dto.Id, []);
            if (model is null)
            {
                response.ErrorMessages!.Add("لايوجد بيانات لحذفها");
                return BadRequest(response);
            }
            else
            {
                await _unitOfWork.ModelRepository.DeleteAsync(dto.Id);
                await _unitOfWork.CompleteAsync();

                response.IsSuccess = true;
                response.Message = "تم الحذف بنجاح";

                return Ok(response);
            }
        }
    }

}
