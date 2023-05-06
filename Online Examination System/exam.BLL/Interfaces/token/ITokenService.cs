using exam.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exam.BLL.Interfaces.token
{
    public interface ITokenService
    {
        Task<string> CreateToken(ApplicationUser appUser, UserManager<ApplicationUser> userManager);
    }
}
