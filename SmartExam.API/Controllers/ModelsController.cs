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
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            ApiResponse<IReadOnlyList<ModelDTO>> response = new ApiResponse<IReadOnlyList<ModelDTO>>();

            IReadOnlyList<Model> result = await _unitOfWork.ModelRepository.GetAllAsync();
            IReadOnlyList<ModelDTO> dto = _mapper.Map<IReadOnlyList<ModelDTO>>(result);

            response.IsSuccess = true;
            response.Data = dto;

            return Ok(response);
        }

        // GET api/<ModelsController>/5
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            ApiResponse<ModelDTO> response = new ApiResponse<ModelDTO>();

            Model result = await _unitOfWork.ModelRepository.GetByIdAsync(id, []);
            ModelDTO dto = _mapper.Map<ModelDTO>(result);

            response.IsSuccess = true;
            response.Data = dto;

            return Ok(response);
        }

        // POST api/<ModelsController>
        [HttpPost("Add")]
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
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateModelDTO dto)
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
        public async Task<IActionResult> Delete([FromBody] DeleteModelDTO dto)
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
