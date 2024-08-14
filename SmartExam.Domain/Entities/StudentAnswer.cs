using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartExam.Domain.Entities
{
    public class StudentAnswer: BaseEntity
    {
        public required int StudentExamId { get; set; }
        public required int  QuestionId { get; set; }
        public string? SelectedAnswer { get; set; }
        public bool IsCorrect { get; set; } = false;

        // Relations
        public virtual StudentExam? StudentExam { get; set; }
        public virtual Question? Question { get; set; }
    }
}
