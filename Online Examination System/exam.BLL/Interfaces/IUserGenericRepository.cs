using exam.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exam.BLL.Interfaces
{
    public interface IUserGenericRepository<T> where T : ApplicationUser
    {
        Task<IReadOnlyList<T>> GetAllAsync();
    }
}
