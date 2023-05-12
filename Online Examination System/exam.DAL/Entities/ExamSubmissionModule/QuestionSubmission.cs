using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using exam.DAL.Entities.QuestionsModule;

namespace exam.DAL.Entities.ExamSubmissionModule
{
    public class QuestionSubmission : BaseEntity
    {
        public int QuestionId { get; set; }
        public IQuestionSubmission Submission { get; set; }
        public bool IsCorrect { get; set; }
        public QuestionMarkedStatus QuestionMarkedStatus { get; set; }

        public QuestionTypes QuestionTypes { get; set; }

        public int ExamCopyId { get; set; }//fk
        [ForeignKey(nameof(ExamCopyId))]
        public ExamSubmission ExamSubmission { get; set; }
    }
}
