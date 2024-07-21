using SmartExam.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartExam.Application.DTOs.Subject
{
    public record SubjectDTO (
      int Id,
      string Name,
      string UserId,
      object? User = null
    );
}
