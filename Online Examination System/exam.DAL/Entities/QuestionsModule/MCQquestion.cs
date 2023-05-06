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
        public List<string> Options { get; set; } = new List<string>();
       
        
    }
}
