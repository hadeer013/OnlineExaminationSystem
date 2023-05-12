using exam.DAL.Entities.QuestionsModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exam.BLL.Services
{
    public class QuestionTypeWithCount
    {
        public QuestionTypes QuestionType { get; set; }
        public int QuestionCount { get; set; }
    }
}
