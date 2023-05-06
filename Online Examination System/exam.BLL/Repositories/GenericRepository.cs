using exam.BLL.Interfaces;
using exam.BLL.Specifications;
using exam.DAL.Data;
using exam.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exam.BLL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly ExaminationContext context;

        public GenericRepository(ExaminationContext context)
        {
            this.context = context;
        }

        public async Task<int> Add(T type)
        {
            context.Set<T>().Add(type);
            return await context.SaveChangesAsync();
        }

        public async Task<int> Delete(T type)
        {
            context.Set<T>().Remove(type);
            return await context.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
          return await context.Set<T>().FindAsync(id);
        }
        
        public async Task<int> Update(T type)
        {
            context.Update(type);
            return await context.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllWithSpec(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }
        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.BuildQuery(context.Set<T>(), spec);
        }

        public async Task<int> GetCountAsync(ISpecification<T> spec)
        => await ApplySpecification(spec).CountAsync();

        public async Task<T> GetByIdWithSpec(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }
    }
}
