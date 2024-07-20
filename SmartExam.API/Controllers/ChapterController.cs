
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SmartExam.Application.DTOs.Chapter;
using SmartExam.Application.Interfaces.Repositories;
using SmartExam.Domain.Entities;
using System;

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
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ChapterController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
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

            Chapter model = _mapper.Map<Chapter>(dto);
            await _unitOfWork.ChapterRepository.AddAsync(model);
            await _unitOfWork.CompleteAsync();

            return Ok(model);
        }

        // PUT api/<ChapterController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ChapterController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
