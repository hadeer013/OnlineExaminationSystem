using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exam.DAL.Entities.QuestionsModule.MCQModule
{
    [Table("MCQwithMultipleCorrectAns")]
    public class MCQwithMultipleCorrectAns:MCQquestion
    {
        public ICollection<Answer> Answers { get; set; }=new HashSet<Answer>();
    }
}
