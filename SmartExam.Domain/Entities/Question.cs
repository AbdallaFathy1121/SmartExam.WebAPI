using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartExam.Domain.Entities
{
    public class Question: BaseEntity
    {
        public Question()
        {
            StudentExamQuestions = new HashSet<StudentExamQuestion>();
            StudentAnswers = new HashSet<StudentAnswer>();
        }

        public required string QuestionName { get; set; }
        public required string Answer1 { get; set; }
        public required string Answer2 { get; set; }
        public string? Answer3 { get; set; }
        public string? Answer4 { get; set; }
        public required string CorrectAnswer { get; set; }
        public required int ModelId { get; set; }

        // Relations
        public virtual Model? Model { get; set; } = null;
        public virtual ICollection<StudentExamQuestion>? StudentExamQuestions { get; set; }
        public virtual ICollection<StudentAnswer>? StudentAnswers { get; set; }
    }
}
