using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartExam.Domain.Entities
{
    public class Chapter: BaseEntity
    {
        public required string Name { get; set; }
        public required int SubjectId { get; set; }

        // Relations
        public virtual Subject? Subject { get; set; } = null;
    }
}
