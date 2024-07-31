using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartExam.Domain.Entities
{
    public class Model: BaseEntity
    {
        public Model()
        {
            Questions = new HashSet<Question>();    
            ExamQueries = new HashSet<ExamQuery>();
        }

        public required string Name { get; set; }
        public required int ChapterId { get; set; }

        // Relations
        public virtual Chapter? Chapter { get; set; } = null;
        public virtual ICollection<Question>? Questions { get; set; }
        public virtual ICollection<ExamQuery>? ExamQueries { get; set; }
    }
}
