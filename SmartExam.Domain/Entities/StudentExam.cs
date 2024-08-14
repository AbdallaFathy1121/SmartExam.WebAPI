using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartExam.Domain.Entities
{
    public class StudentExam: BaseEntity
    {
        public StudentExam()
        {
            StudentExamQuestions = new HashSet<StudentExamQuestion>();
            StudentAnswers = new HashSet<StudentAnswer>();
        }

        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required int ExamId { get; set; }

        // Relations
        public virtual Exam? Exam { get; set; }
        public virtual ICollection<StudentExamQuestion>? StudentExamQuestions { get; set; }
        public virtual ICollection<StudentAnswer>? StudentAnswers { get; set; }
    }
}
