using AutoMapper;
using SmartExam.Application.DTOs.Exam;
using SmartExam.Application.DTOs.Model;
using SmartExam.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartExam.Application.AutoMapper
{
    public class ExamProfile: Profile
    {
        public ExamProfile()
        {
            CreateMap<AddExamDTO, Exam>()
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate));

            CreateMap<Model, ExamDTO>();
        }
    }
}
