using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exam.DAL.Entities
{
    [Table("Student")]
    public class Student:ApplicationUser
    {

        public Level? Level { get; set; }
        public Department? Department { get; set; }
    }
}
