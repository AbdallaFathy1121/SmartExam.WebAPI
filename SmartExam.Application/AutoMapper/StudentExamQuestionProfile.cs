using AutoMapper;
using SmartExam.Application.DTOs.Chapter;
using SmartExam.Application.DTOs.StudentExamQuestion;
using SmartExam.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartExam.Application.AutoMapper
{
    public class StudentExamQuestionProfile: Profile
    {
        public StudentExamQuestionProfile()
        {
            CreateMap<StudentExamQuestion, StudentExamQuestionDTO>();
        }
    }
}
