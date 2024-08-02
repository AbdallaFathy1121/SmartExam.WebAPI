﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartExam.Application.DTOs.ExamQuery
{
    public record UpdateExamQueryDTO (
        int ExamId,
        int ChapterId,
        int ModelId,
        int QuestionNumbers
    );
}