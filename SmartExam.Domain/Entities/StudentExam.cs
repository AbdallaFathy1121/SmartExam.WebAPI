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
        [MinLength(7, ErrorMessage = "Should more than 7 char")]
        public required string FullName { get; set; }

        [EmailAddress(ErrorMessage ="write Correct Email")]
        public required string Email { get; set; }
        public required int ExamId { get; set; }

        // Relations
        public virtual Exam? Exam { get; set; }
    }
}
