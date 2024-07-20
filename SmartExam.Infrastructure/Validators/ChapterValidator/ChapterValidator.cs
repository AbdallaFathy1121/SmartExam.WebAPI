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
                .NotEmpty().NotNull().WithMessage("هذا الحقل مطلوب")
                .MinimumLength(10).WithMessage("يجب كتابة الاسم بالكامل");
        }
    }
}
