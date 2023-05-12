using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exam.DAL.Entities.QuestionsModule
{
    [Table("OpenEndedQuestion")]
    public class OpenEndedQuestion:Question
    {
        public override QuestionTypes QuestionType { get; } = QuestionTypes.OpenEnded;
        public string? Answer { get; set; }
    }
}
