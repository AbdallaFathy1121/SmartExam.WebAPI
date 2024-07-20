
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SmartExam.Application.DTOs.Chapter;
using SmartExam.Application.Interfaces.Repositories;
using SmartExam.Domain.Entities;
using System;
using System.Collections.Generic;

namespace SmartExam.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChapterController : ControllerBase
    {
        private IValidator<Chapter> _validator;
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public ChapterController(IValidator<Chapter> validator, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _validator = validator;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        // GET: api/<ChapterController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IReadOnlyList<Chapter> result = await _unitOfWork.ChapterRepository.GetAllAsync();
            IReadOnlyList<ChapterDTO>  dto = _mapper.Map<IReadOnlyList<ChapterDTO>>(result);

            return Ok(dto);
        }

        // GET api/<ChapterController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            Chapter result = await _unitOfWork.ChapterRepository.GetByIdAsync(id, []);
            ChapterDTO dto = _mapper.Map<ChapterDTO>(result);

            return Ok(dto);
        }

        // POST api/<ChapterController>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddChapterDTO dto)
        {
            var exists = await _unitOfWork.ChapterRepository.GetWhereAsync(a => a.Name == dto.Name); 
            if (exists.Count() > 0)
            {
                return BadRequest("هذا الاسم موجود بالفعل");
            }
            else
            {
                Chapter chapter = _mapper.Map<Chapter>(dto);

                var validationResult = await _validator.ValidateAsync(chapter);
                if (!validationResult.IsValid)
                {
                    List<string> errors = new List<string>();
                    foreach (var error in validationResult.Errors)
                    {
                        errors.Add(error.ErrorMessage);
                    }
                    return BadRequest(errors);
                }
                else
                {
                    await _unitOfWork.ChapterRepository.AddAsync(chapter);
                    await _unitOfWork.CompleteAsync();

                    return Ok(chapter);
                }
            }
        }

        // PUT api/<ChapterController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateChapterDTO dto)
        {
            Chapter chapter = await _unitOfWork.ChapterRepository.GetByIdAsync(id, []);
            if (chapter is null)
            {
                return NotFound("لايوجد فصل");
            }
            else
            {
                chapter.Name = dto.Name;
            
                var validationResult = await _validator.ValidateAsync(chapter);
                if (!validationResult.IsValid)
                {
                    List<string> errors = new List<string>();
                    foreach (var error in validationResult.Errors)
                    {
                        errors.Add(error.ErrorMessage);
                    }
                    return BadRequest(errors);
                }
                else
                {
                    await _unitOfWork.ChapterRepository.UpdateAsync(id, chapter);
                    await _unitOfWork.CompleteAsync();

                    return Ok(chapter);
                }
            }
        }

        // DELETE api/<ChapterController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _unitOfWork.ChapterRepository.GetByIdAsync(id, []);
            if (result == null)
            {
                return BadRequest("لايوجد بيانات لمسحها");
            }
            else
            {
                await _unitOfWork.ChapterRepository.DeleteAsync(id);
                await _unitOfWork.CompleteAsync();

                return Ok("تم الحذف بنجاح");
            }
        }
    }
}
