using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartExam.Application.DTOs.StudentExamQuestion
{
    public record StudentExamQuestionDTO (
        int Id,
        int StudentExamId,
        int QuestionId
    );
}
