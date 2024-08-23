using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartExam.Domain.Entities
{
    public class ExamResults: BaseEntity
    {
        public required int StudentExamID { get; set; }
        public required int TotalScore { get; set; }
        public required int StudentDegree { get; set; }
        public required float Percent { get; set; }
        public bool Status { get; set; } = false;

        // Relations
        public virtual StudentExam? StudentExam { get; set; }
    }
}
