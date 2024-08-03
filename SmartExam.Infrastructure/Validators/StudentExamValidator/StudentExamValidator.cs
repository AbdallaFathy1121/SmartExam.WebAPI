using FluentValidation;
using SmartExam.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartExam.Infrastructure.Validators.StudentExamValidator
{
    public class StudentExamValidator: AbstractValidator<StudentExam>
    {
        public StudentExamValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().NotNull().WithMessage("Full Name is Required!")
                .Length(10, 250).WithMessage("Name can't be less than 10 characters. and longer than 250 characters");

            RuleFor(x => x.Email)
                .NotNull().WithMessage("Email is Required!")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.ExamId)
                .NotNull().WithMessage("Exam Id is Required!");
        }
    }
}
