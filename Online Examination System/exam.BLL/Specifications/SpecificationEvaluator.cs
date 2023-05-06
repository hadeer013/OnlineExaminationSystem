using exam.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exam.BLL.Specifications
{
    public class SpecificationEvaluator<T> where T : BaseEntity
    {
        public static IQueryable<T> BuildQuery(IQueryable<T> InputQuery,ISpecification<T> Spec)
        {
            var query = InputQuery;

            query = Spec.Includes.Aggregate(query, (start, include) => start.Include(include));

            if (Spec.Criteria != null)
                query = query.Where(Spec.Criteria);

            if (Spec.OrderBy !=null)
                query = query.OrderBy(Spec.OrderBy);

            if (Spec.OrderByDesc != null)
                query = query.OrderByDescending(Spec.OrderByDesc);

            if(Spec.IsPaginated)
             query = query.Skip(Spec.Skip).Take(Spec.Take);

            return query;

        }
    }
}
