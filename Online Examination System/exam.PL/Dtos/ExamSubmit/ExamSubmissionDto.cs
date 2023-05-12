using exam.DAL.Entities.ExamEntities;
using exam.DAL.Entities.ExamSubmissionModule;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace exam.PL.Dtos.ExamSubmit
{
    public class ExamSubmissionDto
    {
        public int ExamCopyId { get; set; }//Pk
        public int ExamId { get; set; }
        public ExamType ExamType { get; set; }
        public float TotalMark { get; set; }
        public bool IsReviewed { get; set; }
    }
}
