using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartExam.Application.DTOs.ExamQuery
{
    public record ExamQueryDTO (
        int Id,
        int ExamId,
        int ModelId,
        int QuestionNumbers,
        object? Exam = null,
        object? Model = null
    );
}
