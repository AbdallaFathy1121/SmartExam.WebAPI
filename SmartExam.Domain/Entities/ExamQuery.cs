using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartExam.Domain.Entities
{
    public class ExamQuery : BaseEntity
    {
        public required int ExamId { get; set; }
        public required int ModelId { get; set; }

        [Range(1, 100, ErrorMessage ="Number of Questions should be between (1, 100)")]
        public required int QuestionNumbers { get; set; }


        // Realtions
        public virtual Exam? Exam { get; set; }
        public virtual Model? Model { get; set; }
    }
}
