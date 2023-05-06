using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exam.DAL.Entities.QuestionsModule
{
    public class Question:BaseEntity
    {
        public string? QuestionFormFileUrl { get; set; }
        public string? questionContent { get; set; }
        public QustionDifficulty QuestionDifficulty { get; set; }
        public virtual QuestionTypes QuestionType { get; }
        public int ChapterId { get; set; }

        [ForeignKey(nameof(ChapterId))]
        public Chapter? Chapter { get; set; }
    }
}
