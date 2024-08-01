using AutoMapper;
using SmartExam.Application.DTOs.ExamQuery;
using SmartExam.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartExam.Application.AutoMapper
{
    public class ExamQueryProfile : Profile
    {
        public ExamQueryProfile()
        {
            CreateMap<AddExamQueryDTO, ExamQuery>()
                .ForMember(src => src.Id, opt => opt.Ignore());

            CreateMap<ExamQuery, ExamQueryDTO>();
        }
    }
}
