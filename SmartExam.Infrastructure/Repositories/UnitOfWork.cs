using Namshi.Infrastructure.Context;
using SmartExam.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartExam.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IChapterRepository ChapterRepository { get; private set; }


        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            ChapterRepository = new ChapterRepository(context);
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async void Dispose()
        {
            await _context.DisposeAsync();
        }
    }
}
