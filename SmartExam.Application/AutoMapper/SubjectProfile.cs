using AutoMapper;
using Microsoft.AspNetCore.Routing.Constraints;
using SmartExam.Application.DTOs.Chapter;
using SmartExam.Application.DTOs.Subject;
using SmartExam.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartExam.Application.AutoMapper
{
    public class SubjectProfile: Profile
    {
        public SubjectProfile()
        {
            CreateMap<AddSubjectDTO, Subject>();

            CreateMap<Subject, SubjectDTO>();
        }
    }
}
