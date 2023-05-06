using exam.DAL.Entities;
using exam.DAL.Entities.ProfessorEntities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exam.DAL.Data
{
    public class ExaminationContext : IdentityDbContext<ApplicationUser>
    {
        public ExaminationContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Level>? Levels { get; set; }
        public DbSet<Department>? Departments { get; set; }
        public DbSet<Subject>? Subjects { get; set; }
        public DbSet<Admin>? Admins  { get; set; }
        public DbSet<Student>? Students { get; set; }
        public DbSet<Professor>? Professors { get; set; }
        public DbSet<ProfessorSignUpRequests>? professorSignUpRequests { get; set; }

    }
}
