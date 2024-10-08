﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartExam.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Namshi.Infrastructure.Context
{
    public class ApplicationDbContext: IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        { 
        }

        public virtual DbSet<Chapter> Chapters { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<Model> Models { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Exam> Exams { get; set; }
        public virtual DbSet<ExamQuery> ExamQueries { get; set; }
        public virtual DbSet<StudentExam> StudentExams { get; set; }
        public virtual DbSet<StudentExamQuestion> StudentExamQuestions { get; set; }
        public virtual DbSet<StudentAnswer> StudentAnswers { get; set; }
        public virtual DbSet<ExamResults> ExamResults { get; set; }
    }
}
