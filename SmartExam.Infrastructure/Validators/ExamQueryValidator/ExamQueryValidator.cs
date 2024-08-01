using FluentValidation;
using SmartExam.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartExam.Infrastructure.Validators.ExamQueryValidator
{
    public class ExamQueryValidator: AbstractValidator<ExamQuery>
    {
        public ExamQueryValidator()
        {
            RuleFor(x => x.ExamId)
                .NotNull().WithMessage("Exam ID is Required");

            RuleFor(x => x.ChapterId)
                .NotNull().WithMessage("Chapter Id is Required!");

            RuleFor(x => x.ModelId)
                .NotNull().WithMessage("Model ID is Required!");

            RuleFor(x => x.QuestionNumbers)
                .NotNull().NotEmpty().WithMessage("Question Numbers is Required!");
        }
    }
}
