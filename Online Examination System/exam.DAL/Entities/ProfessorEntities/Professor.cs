using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exam.DAL.Entities.ProfessorEntities
{
    [Table("Professor")]
    public class Professor : ApplicationUser
    {
        public ICollection<Subject> Subjects { get; set; } = new HashSet<Subject>();
    }
}
