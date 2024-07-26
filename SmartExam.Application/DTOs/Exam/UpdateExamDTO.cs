using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartExam.Application.DTOs.Exam
{
    public record UpdateExamDTO (
        string Name,
        string StartDate,
        string StartTime,
        int DurrationTime,
        bool Status
    );
}
