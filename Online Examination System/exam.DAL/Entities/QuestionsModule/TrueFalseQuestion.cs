using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exam.DAL.Entities.QuestionsModule
{
    public class TrueFalseQuestion:Question
    {
        public override QuestionTypes QuestionType { get; } = QuestionTypes.TrueAndFalse;
        public bool CorrectAnswer { get; set; }
    }
}
