using FluentValidation;
using SmartExam.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartExam.Infrastructure.Validators.ExamValidator
{
    public class ExamValidator: AbstractValidator<Exam>
    {
        public ExamValidator() 
        {
            RuleFor(x => x.Name)
                .NotEmpty().NotNull().WithMessage("Name is Required!");

            RuleFor(x => x.StartDate)
                .NotNull().WithMessage("Start Date is Required!");

            RuleFor(x => x.StartTime)
                .NotNull().WithMessage("Start Time is Required!");

            RuleFor(x => x.DurrationTime)
                .NotNull().WithMessage("Duration Time is Required!");

            RuleFor(x => x.UserId)
                .NotNull().WithMessage("user Id is Required!");
        }
    }
}
