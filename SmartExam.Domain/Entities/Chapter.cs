using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartExam.Domain.Entities
{
    public class Chapter: BaseEntity
    {
        public required string Name { get; set; }
    }
}
