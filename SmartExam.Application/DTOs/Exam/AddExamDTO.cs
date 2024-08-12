using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartExam.Application.DTOs.Exam
{
    public record AddExamDTO (
        string Name,
        string StartDate,  // Note: StartDate is a string here formate => "15-08-2024"
        string StartTime,  // Note: StartTime is a string here formate => "12:20"
        int DurrationTime,
        string userId,
        int subjectId
    );  
}
