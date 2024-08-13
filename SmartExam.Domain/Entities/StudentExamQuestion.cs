using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartExam.Domain.Entities
{
    public class StudentExamQuestion: BaseEntity
    {
        public required int QuestionId { get; set; }
        public required int StudentExamId { get; set; }

        // Relations
        public virtual Question? Question { get; set; }
        public virtual StudentExam? StudentExam { get; set; }
    }
}
