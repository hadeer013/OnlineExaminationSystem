using exam.BLL.Specifications;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VHW.BLL.Specification;

namespace Talabat.BLL.Specification
{
    public class BaseSpecification<T> : ISpecification<T>
    {
        public Expression<Func<T, bool>> Criteria { get; set; }
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();
        public Expression<Func<T, object>> OrderBy { get; set; }
        public Expression<Func<T, object>> OrderByDesc { get; set; }
        public int Take { get; set; }
        public int Skip { get; set; }
        public bool IsPaginated { get; set; }
        public List<Func<IQueryable<T>, IIncludableQueryable<T, object>>> IncludeThenIncludes { get; set; } 
            = new List<Func<IQueryable<T>, IIncludableQueryable<T, object>>>();

        public BaseSpecification()
        {
                
        }
        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            this.Criteria = criteria;
        }
        public void AddInclude( Expression<Func<T, object>> Include)
        {
            
            Includes.Add(Include);
        }
        public void AddThenInclude(Func<IQueryable<T>, IIncludableQueryable<T, object>> Theninclude)
            => this.IncludeThenIncludes.Add(Theninclude);
        public void AddOrderBy(Expression<Func<T, object>> orderby)
        {
            this.OrderBy = orderby;
        }
        public void AddOrderByDesc(Expression<Func<T, object>> OrderByDesc)
        {
            this.OrderByDesc = OrderByDesc;
        }

        public void ApplyPagination(int skip,int take)
        {
            Take = take;
            Skip = skip;
            IsPaginated = true;
        }

    }
}
