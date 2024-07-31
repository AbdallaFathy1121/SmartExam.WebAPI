using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartExam.Domain.Entities
{
    public class ExamQuery : BaseEntity
    {
        public required int ExamId { get; set; }
        public required int ChapterId { get; set; }
        public required int ModelId { get; set; }
        public required int QuestionNumbers { get; set; }


        // Realtions
        public virtual Exam? Exam { get; set; }
        public virtual Chapter? Chapter { get; set; }
        public virtual Model? Model { get; set; }
    }
}
