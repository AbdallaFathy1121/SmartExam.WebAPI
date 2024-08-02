using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartExam.Application.DTOs.StudentExam
{
    public record StudentExamDTO (
      int Id,
      string FullName,
      string Email,
      int ExamId,
      object? Exam
    );
}
