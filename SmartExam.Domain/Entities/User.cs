using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartExam.Domain.Entities
{
    public class User: IdentityUser
    {
        public User()
        {
            Subjects = new HashSet<Subject>();
            Exams = new HashSet<Exam>();
        }

        public required string Name { get; set; }

        // Relations
        public virtual ICollection<Subject>? Subjects { get; set; }
        public virtual ICollection<Exam>? Exams { get; set; }
    }
}
