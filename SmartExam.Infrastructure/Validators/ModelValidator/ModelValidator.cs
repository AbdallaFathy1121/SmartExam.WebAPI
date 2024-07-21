using FluentValidation;
using SmartExam.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartExam.Infrastructure.Validators.ModelValidator
{
    public class ModelValidator: AbstractValidator<Model>
    {
        public ModelValidator()
        {
            RuleFor(x => x.Name)
                .NotNull().WithMessage("Name is Required");

            RuleFor(x => x.ChapterId)
                .NotEmpty().NotNull().WithMessage("ChapterId is Required");
        }
    }
}
