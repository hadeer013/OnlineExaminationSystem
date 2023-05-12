using exam.DAL.Entities.ProfessorEntities;
using exam.DAL.Entities.QuestionsModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace exam.DAL.Entities.ExamEntities
{
    public class Exam:BaseEntity
    {
        public string? ExamName { get; set; }
        public ExamType ExamType { get; set; }
        public DateTime ExamStart { get; set; }
        public DateTime ExamEnd { get; set; }
        public int Duration { get; set; }
        public float TotalExamPoints { get; set; }
        public ICollection<QuestionTypePoint> QuestionTypePoints { get; set; }

        public string? InitiatorId { get; set; }

        [ForeignKey(nameof(InitiatorId))]
        public ApplicationUser? Initiator { get; set; }

        public int SubjectId { get; set; }
        [ForeignKey(nameof(SubjectId))]
        public Subject? Subject { get; set; }

        public ICollection<ExamQuestionsDistribution> examQuestionsDistributions { get; set; }=new HashSet<ExamQuestionsDistribution>();
    }
}
