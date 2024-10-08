﻿using Microsoft.EntityFrameworkCore;
using Namshi.Infrastructure.Context;
using SmartExam.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SmartExam.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private ApplicationDbContext _context;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity is not null)
            {
                _context.Set<T>().Remove(entity);
            }
        }

        public async Task<T> GetFirstAsync(Expression<Func<T, bool>> match)
        {
            var result = await _context.Set<T>().FirstOrDefaultAsync(match);

            return result;
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>();
            // Apply any includes
            foreach (var item in includes)
            {
                query = query.Include(item);
            }

            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>();
            // Apply any includes
            foreach (var item in includes)
            {
                query = query.Include(item);
            }
            return await ((DbSet<T>)query).FindAsync(id);
        }

        public async Task<IList<T>> GetWhereAsync(Expression<Func<T, bool>> match, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>();
            // Apply any includes
            foreach (var item in includes)
            {
                query = query.Include(item);
            }
            var result = await query.Where(match).AsNoTracking().ToListAsync();

            return result;
        }

        public async Task UpdateAsync(int id, T entity)
        {
            var existingEntity = await _context.Set<T>().FindAsync(id);
            if (existingEntity is not null)
            {
                _context.Set<T>().Update(entity);
            }
        }
    }
}
