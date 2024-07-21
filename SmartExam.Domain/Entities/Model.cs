using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartExam.Domain.Entities
{
    public class Model: BaseEntity
    {
        public required string Name { get; set; }
        public required int ChapterId { get; set; }

        // Relations
        public virtual Chapter? Chapter { get; set; } = null;
    }
}
