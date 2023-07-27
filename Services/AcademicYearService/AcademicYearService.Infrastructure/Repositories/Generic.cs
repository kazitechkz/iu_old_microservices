using AcademicYearService.Application.Core.Interfaces;
using AcademicYearService.Application.Core.Specification;
using AcademicYearService.Domain.Models;
using AcademicYearService.Infrastructure.Specifications;
using Microsoft.EntityFrameworkCore;

namespace AcademicYearService.Infrastructure.Repositories;

public class Generic<T> : IGeneric<T> where T : BaseModel
    {
        private readonly DataContext _context;

        public Generic(DataContext context)
        {
            _context = context;
        }

        public async Task<T> GetByIdAsync(long id)
        {
            return await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();

        }

        public async Task<T> GetEntityWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<T>> ListWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).AsNoTracking().ToListAsync();
        }

        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            _context.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
            _context.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        protected IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);
        }

        public IQueryable<T> GetQueryable(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_context.Set<T>(), spec);
        }
    }