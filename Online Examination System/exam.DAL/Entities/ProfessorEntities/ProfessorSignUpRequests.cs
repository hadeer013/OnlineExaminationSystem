using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exam.DAL.Entities.ProfessorEntities
{
    public class ProfessorSignUpRequests:BaseEntity
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? UserName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public bool? IsApproved { get; set; } = false;
    }
}
