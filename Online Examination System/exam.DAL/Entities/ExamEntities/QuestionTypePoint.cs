using exam.DAL.Entities.QuestionsModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exam.DAL.Entities.ExamEntities
{
    public class QuestionTypePoint : BaseEntity
    {
        public QuestionTypes QuestionType { get; set; }
        public float Point { get; set; }

        public int ExamId { get; set; }
        [ForeignKey(nameof(ExamId))]
        public Exam Exam { get; set; }
    }
}
