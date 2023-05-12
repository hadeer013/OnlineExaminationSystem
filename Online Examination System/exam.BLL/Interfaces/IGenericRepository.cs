using exam.BLL.Specifications;
using exam.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exam.BLL.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T> Add(T type);
        Task<T> Update(T type);
        Task<int> Delete(T type);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> GetAllWithSpec(ISpecification<T> spec);
        Task<int> GetCountAsync(ISpecification<T> spec);
        Task<T> GetByIdWithSpec(ISpecification<T> spec);
    }
}
