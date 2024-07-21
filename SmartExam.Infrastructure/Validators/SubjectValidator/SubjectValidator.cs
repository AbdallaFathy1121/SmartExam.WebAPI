using FluentValidation;
using SmartExam.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartExam.Infrastructure.Validators.ChapterValidator
{
    public class SubjectValidator : AbstractValidator<Subject>
    {
        public SubjectValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("يجب كتابة الاسم")
                .NotNull().WithMessage("هذا الحقل مطلوب");

            RuleFor(x => x.UserId)
                .NotNull().WithMessage("هذا الحقل مطلوب");
        }
    }
}
