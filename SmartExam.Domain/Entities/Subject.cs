﻿using System;
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
        }

        public required string Name { get; set; }
        public required string UserId { get; set; }

        // Relations
        public virtual User? User { get; set; } = null;
        public ICollection<Chapter>? Chapters { get; set; }
    }
}