using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using exam.DAL.Entities.ExamEntities;

namespace exam.DAL.Entities.ExamSubmissionModule
{
    public class ExamSubmission
    {
        [Key]
        public int ExamCopyId { get; set; }//Pk
        
        public int ExamId { get; set; }
        [ForeignKey(nameof(ExamId))]
        public Exam? Exam { get; set; }
        public ExamType ExamType { get; set; } 
        public ICollection<QuestionSubmission> QuestionSubmissions { get; set; }
        public string? StudentId { get; set; }

        [ForeignKey(nameof(StudentId))]
        public Student? Student { get; set; }
        public float TotalMark { get; set; }
        public bool IsReviewed { get; set; }
    }
}
