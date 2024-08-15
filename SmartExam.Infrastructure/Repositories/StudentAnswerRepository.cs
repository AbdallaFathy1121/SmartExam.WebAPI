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
    public class StudentAnswerRepository : GenericRepository<StudentAnswer>, IStudentAnswerRepository
    {
        private readonly ApplicationDbContext _context;
        public StudentAnswerRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
