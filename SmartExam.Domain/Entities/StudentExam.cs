using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartExam.Domain.Entities
{
    public class StudentExam: BaseEntity
    {
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required int ExamId { get; set; }

        // Relations
        public virtual Exam? Exam { get; set; }
    }
}
