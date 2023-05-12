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
        public string? Code { get; set; }
        public int LevelId { get; set; }
        [ForeignKey(nameof(LevelId))]
        public Level? Level { get; set; }
        public int DepartmentId { get; set; }
        [ForeignKey(nameof(DepartmentId))]
        public Department? Department { get; set; }
    }
}
