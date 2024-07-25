﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartExam.Domain.Entities
{
    public class Exam: BaseEntity
    {
        public required string Name { get; set; }
        public required DateOnly StartDate { get; set; }
        public required TimeOnly StartTime { get; set; }
        public required int DurationTime { get; set; }
        public required string UserId { get; set; }

        // Relations
        public virtual User? User { get; set; }
    }
}