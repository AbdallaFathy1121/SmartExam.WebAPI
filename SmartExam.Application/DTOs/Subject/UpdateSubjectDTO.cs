using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartExam.Application.DTOs.Subject
{
    public record UpdateSubjectDTO(
      string Name,
      string UserId
    );
}
