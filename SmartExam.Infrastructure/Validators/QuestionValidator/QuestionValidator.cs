using FluentValidation;
using SmartExam.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartExam.Infrastructure.Validators.QuestionValidator
{
    public class QuestionValidator: AbstractValidator<Question>
    {
        public QuestionValidator() 
        {
            RuleFor(x => x.QuestionName)
                .NotEmpty().NotNull().WithMessage("Question Name is Required!");

            RuleFor(x => x.Answer1)
                .NotEmpty().NotNull().WithMessage("Answer1 is Required!");

            RuleFor(x => x.Answer2)
                .NotEmpty().NotNull().WithMessage("Answer2 is Required!");

            RuleFor(x => x.ModelId)
                .NotNull().WithMessage("Model Id is Required!");

            RuleFor(x => x.CorrectAnswer)
                .NotNull().WithMessage("Correct Answer is Required!");
        }
    }
}
