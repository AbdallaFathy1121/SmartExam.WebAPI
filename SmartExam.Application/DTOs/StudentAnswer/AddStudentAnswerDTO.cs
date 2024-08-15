﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartExam.Application.DTOs.StudentAnswer
{
    public record AddStudentAnswerDTO (
        int StudentExamId,
        int QuestionId,
        string SelectedAnswer
    );
}