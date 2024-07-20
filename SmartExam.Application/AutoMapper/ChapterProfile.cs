using AutoMapper;
using SmartExam.Application.DTOs.Chapter;
using SmartExam.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartExam.Application.AutoMapper
{
    public class ChapterProfile: Profile
    {
        public ChapterProfile()
        {
            CreateMap<AddChapterDTO, Chapter>()
                .ForMember(src => src.Id, opt => opt.Ignore());

            CreateMap<Chapter, ChapterDTO>();
        }
    }
}
