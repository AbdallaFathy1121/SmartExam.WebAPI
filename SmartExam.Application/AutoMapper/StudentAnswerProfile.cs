using AutoMapper;
using SmartExam.Application.DTOs.StudentAnswer;
using SmartExam.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartExam.Application.AutoMapper
{
    public class StudentAnswerProfile: Profile
    {
        public StudentAnswerProfile()
        {
            CreateMap<StudentAnswer, StudentAnswerDTO>();
        }
    }
}
