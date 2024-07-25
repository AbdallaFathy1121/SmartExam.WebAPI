using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartExam.Application.DTOs.Exam
{
    public record ExamDTO (
        int Id,
        string Name,
        DateOnly StartDate,
        TimeOnly StartTime,
        int DurrationTime,
        string userId,
        object? User = null
    );
}
