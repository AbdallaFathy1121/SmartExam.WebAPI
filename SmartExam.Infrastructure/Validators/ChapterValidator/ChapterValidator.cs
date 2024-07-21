using FluentValidation;
using SmartExam.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartExam.Infrastructure.Validators.ChapterValidator
{
    public class ChapterValidator : AbstractValidator<Chapter>
    {
        public ChapterValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("يجب كتابة الاسم")
                .NotNull().WithMessage("Name is Required");

            RuleFor(x => x.SubjectId)
                .NotNull().WithMessage("SubjectId is Required");
        }
    }
}
