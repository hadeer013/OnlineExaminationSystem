using exam.DAL.Entities.ProfessorEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exam.DAL.Entities
{
    public class Subject:BaseEntity
    {
        public string? Name { get; set; }
        public string? ProfessorId { get; set; }

        [ForeignKey(nameof(ProfessorId))]
        public Professor? Professor { get; set; }
        public ICollection<Chapter>? Chapters { get; set; }

        [Required]
        public int LevelId { get; set; }
        [ForeignKey(nameof(LevelId))]
        public Level? Level { get; set; }

        [Required]
        public int DepartmentId { get; set; }
        [ForeignKey(nameof(DepartmentId))]
        public Department? Department { get; set; }

    }
}
