﻿using SmartExam.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartExam.Application.Interfaces.Services
{
    public interface IChangeExamStatus
    {
        Task<bool> ChangeExamStatu(Exam exam, bool status);
    }
}