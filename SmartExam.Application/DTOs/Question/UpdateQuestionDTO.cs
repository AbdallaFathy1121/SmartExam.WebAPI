using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartExam.Application.DTOs.Question
{
    public record UpdateQuestionDTO (
      string QuestionName,
      string Answer1,
      string Answer2,
      string Answer3,
      string Answer4,
      string CorrectAnswer
    );
}
