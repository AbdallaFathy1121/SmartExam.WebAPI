using Hangfire;
using Namshi.Infrastructure.Context;
using SmartExam.Application.Interfaces.Repositories;
using SmartExam.Application.Interfaces.Services;
using SmartExam.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartExam.Infrastructure.Services
{
    public class ChangeExamStatus : IChangeExamStatus
    {
        private readonly ApplicationDbContext _context;
        public ChangeExamStatus(ApplicationDbContext context)
        {
            _context = context;
        }


        public void ChangeExamStatu(Exam exam, bool status)
        {
            exam.Status = status;
            _context.Exams.Update(exam);
            _context.SaveChanges();
        }
    }
}
