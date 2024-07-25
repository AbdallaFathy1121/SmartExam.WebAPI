using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartExam.Domain.Entities
{
    public class Subject: BaseEntity
    {
        public Subject()
        {
            Chapters = new HashSet<Chapter>();
            Exams = new HashSet<Exam>();
        }

        public required string Name { get; set; }
        public required string UserId { get; set; }

        // Relations
        public virtual User? User { get; set; } = null;
        public virtual ICollection<Chapter>? Chapters { get; set; }
        public virtual ICollection<Exam>? Exams { get; set; }
    }
}