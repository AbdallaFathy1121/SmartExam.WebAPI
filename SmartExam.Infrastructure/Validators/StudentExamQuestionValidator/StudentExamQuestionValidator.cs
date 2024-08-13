﻿using FluentValidation;
using SmartExam.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartExam.Infrastructure.Validators.StudentExamQuestionValidator
{
    public class StudentExamQuestionValidator: AbstractValidator<StudentExamQuestion>
    {
        public StudentExamQuestionValidator()
        {
            RuleFor(x => x.QuestionId)
                .NotEmpty().NotNull().WithMessage("Question Id is Required!");

            RuleFor(x => x.StudentExamId)
                .NotEmpty().NotNull().WithMessage("Student Exam Id is Required!");
        }
    }
}
