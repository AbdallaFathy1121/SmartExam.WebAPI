using Namshi.Infrastructure.Context;
using SmartExam.Application.Interfaces.Repositories;
using SmartExam.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartExam.Infrastructure.Repositories
{
    public class ExamQueryRepository : GenericRepository<ExamQuery>, IExamQueryRepository
    {
        private readonly ApplicationDbContext _context;
        public ExamQueryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
