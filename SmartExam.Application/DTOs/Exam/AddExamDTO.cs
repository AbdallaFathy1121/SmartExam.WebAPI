using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartExam.Application.DTOs.Exam
{
    public record AddExamDTO (
        string Name,
        DateOnly StartDate,
        TimeOnly StartTime,
        int DurrationTime,
        string userId,
        int subjectId
    );  
}
