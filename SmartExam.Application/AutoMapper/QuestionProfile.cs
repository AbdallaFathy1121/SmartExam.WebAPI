using AutoMapper;
using SmartExam.Application.DTOs.Model;
using SmartExam.Application.DTOs.Question;
using SmartExam.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartExam.Application.AutoMapper
{
    public class QuestionProfile: Profile
    {
        public QuestionProfile()
        {
            CreateMap<AddQuestionDTO, Question>();

            CreateMap<Question, QuestionDTO>();
        }
    }
}
