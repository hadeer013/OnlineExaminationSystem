using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exam.DAL.Entities.QuestionsModule.MCQModule
{
    public class MCQwithOneCorrectAns:MCQquestion
    {
        public string? CorrectAnswer { get; set; }
    }
}
