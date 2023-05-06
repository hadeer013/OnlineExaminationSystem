using exam.DAL.Entities.ProfessorEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exam.DAL.Entities.ExamEntities
{
    public class Exam:BaseEntity
    {
        public int Duration { get; set; }
        public string? ProfessorId { get; set; }

        [ForeignKey(nameof(ProfessorId))]
        public Professor? Professor { get; set; }

        public int LevelId { get; set; }
        [ForeignKey(nameof(LevelId))]
        public Level? Level { get; set; }


        public int DepartmentId { get; set; }
        [ForeignKey(nameof(DepartmentId))]
        public Department? Department { get; set; }

        public int SubjectId { get; set; }
        [ForeignKey(nameof(SubjectId))]
        public Subject? Subject { get; set; }

        public ICollection<ExamQuestionsDistribution> examQuestionsDistributions { get; set; }=new HashSet<ExamQuestionsDistribution>();
    }
}
