using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartExam.Domain.Entities;

namespace SmartExam.Application.DTOs.Chapter
{
    public record ChapterDTO (
        int Id,
        string Name,
        int SubjectId,
        object? Subject = null
    );
}
