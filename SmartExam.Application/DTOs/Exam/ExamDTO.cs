using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartExam.Application.DTOs.Exam
{
    public class ExamDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateOnly StartDate { get; set; }
        public TimeOnly StartTime { get; set; }
        public int DurrationTime { get; set; }
        public string UserId { get; set; }
        public int SubjectId { get; set; }
        public bool Status { get; set; }
        public object? User { get; set; }
        public object? Subject { get; set; }

    }
}
