using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartExam.Application.DTOs.Exam
{
    public record AddExamDTO (
        string Name,
        string StartDate,  // Note: StartDate is a string here
        string StartTime,  // Note: StartTime is a string here
        int DurrationTime,
        string userId,
        int subjectId
    );  
}
