using AutoMapper;
using SmartExam.Application.DTOs.Exam;
using SmartExam.Application.DTOs.Model;
using SmartExam.Application.TypeConverter;
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
            // Create map with custom type converters
            CreateMap<string, DateOnly>().ConvertUsing<DateOnlyTypeConverter>();
            CreateMap<string, TimeOnly>().ConvertUsing<TimeOnlyTypeConverter>();


            CreateMap<AddExamDTO, Exam>()
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTime));

            CreateMap<Exam, ExamDTO>()
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTime));
        }
    }
}
