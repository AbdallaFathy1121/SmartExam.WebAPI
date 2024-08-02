using AutoMapper;
using SmartExam.Application.DTOs.ExamQuery;
using SmartExam.Application.DTOs.StudentExam;
using SmartExam.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartExam.Application.AutoMapper
{
    public class StudentExamProfile : Profile
    {
        public StudentExamProfile()
        {
            CreateMap<AddStudentExamDTO, StudentExam>()
                .ForMember(src => src.Id, opt => opt.Ignore());

            CreateMap<StudentExam, StudentExamDTO>();
        }
    }
}
