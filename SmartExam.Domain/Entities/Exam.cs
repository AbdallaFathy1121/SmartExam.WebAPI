using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartExam.Domain.Entities
{
    public class Exam: BaseEntity
    {
        public Exam()
        {
            ExamQueries = new HashSet<ExamQuery>();
            StudentExams = new HashSet<StudentExam>();
        }

        public required string Name { get; set; }
        public required DateOnly StartDate { get; set; }
        public required TimeOnly StartTime { get; set; }
        public required int DurationTime { get; set; }
        public Boolean? Status { get; set; } = false;
        public required string UserId { get; set; }
        public required int SubjectId { get; set; }

        // Relations
        public virtual User? User { get; set; }
        public virtual Subject? Subject { get; set; }
        public virtual ICollection<ExamQuery>? ExamQueries { get; set; }
        public virtual ICollection<StudentExam>? StudentExams { get; set; }
    }
}
