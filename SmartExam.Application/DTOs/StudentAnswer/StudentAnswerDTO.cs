using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartExam.Application.DTOs.StudentAnswer
{
    public record StudentAnswerDTO (
        int Id,
        int StudentExamId,
        int QuestionId,
        string SelectedAnswer,
        bool IsCorrect
    );
}
