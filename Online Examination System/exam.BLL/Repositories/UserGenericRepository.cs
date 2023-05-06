using exam.BLL.Interfaces;
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
    public class UserGenericRepository<T> : IUserGenericRepository<T> where T : ApplicationUser
    {
        private readonly ExaminationContext context;

        public UserGenericRepository(ExaminationContext context)
        {
            this.context = context;
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            var result = await context.Set<T>().ToListAsync();
            return result;
        }
    }
}
