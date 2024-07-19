using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartExam.Domain.Entities
{
    public class BaseEntity
    {
        public required int Id { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
    }
}
