using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exam.DAL.Entities.QuestionsModule.MCQModule
{
    [Table("MCQwithOneCorrectAns")]
    public class MCQwithOneCorrectAns:MCQquestion
    {
        public string? CorrectAnswer { get; set; }
    }
}
