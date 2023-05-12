using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exam.DAL.Entities.QuestionsModule
{
    public class MCQquestion:Question
    {
        public override QuestionTypes QuestionType { get; } = QuestionTypes.MCQ;
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }
    }
}
