using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exam.DAL.Entities.QuestionsModule.MCQModule
{
    public class Answer:BaseEntity
    {
        public string? Text { get; set; }
        public int QuestionId { get; set; }
        [ForeignKey(nameof(QuestionId))]
        public MCQwithMultipleCorrectAns? mCQwithMultipleCorrectAns { get; set; }

    }
}
