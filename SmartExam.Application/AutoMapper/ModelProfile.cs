using AutoMapper;
using SmartExam.Application.DTOs.Model;
using SmartExam.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartExam.Application.AutoMapper
{
    public class ModelProfile: Profile
    {
        public ModelProfile()
        {
            CreateMap<AddModelDTO, Model>();

            CreateMap<Model, ModelDTO>();
        }
    }
}
