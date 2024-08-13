using Microsoft.EntityFrameworkCore;
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

        // Method to get random elements from a list
        private static List<int> GetRandomElements(List<int> list, int count)
        {
            Random rng = new Random();
            return list.OrderBy(x => rng.Next()).Take(count).ToList();
        }

        public async Task<List<int>> GetRandomQuestionIdByQuery(IList<ExamQuery> examQueries)
        {
            List<int> results = new List<int>();

            if (examQueries.Count() <= 0)
            {
                return results;
            }

            foreach (var item in examQueries)
            {
                List<int> listOfQuestionId = await _context.Questions
                    .Where(a => a.ModelId == item.ModelId) 
                    .AsNoTracking()
                    .Select(a => a.Id)
                    .ToListAsync();

                // Shuffle the list and select a subset
                List<int> randomIds = GetRandomElements(listOfQuestionId, item.QuestionNumbers);

                results.AddRange(randomIds);
            }

            return results;
        }
    }
}
