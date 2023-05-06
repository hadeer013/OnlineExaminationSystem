using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exam.DAL.Entities.QuestionsModule.MCQModule
{
    public class MCQwithMultipleCorrectAns:MCQquestion
    {
        public List<string> Answers { get; set; }=new List<string>();
    }
}
