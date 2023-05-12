using exam.DAL.Entities.QuestionsModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHW.BLL.Specification;

namespace exam.BLL.Specifications.QuestionsSpecifications
{
    public class QuestionParam: BaseFilterationParams
    {
        [Required]
        public int? ChapterId { get; set; }
        public QuestionTypes? QuestionTypes { get; set; }
        public QustionDifficulty? QustionDifficulty { get; set; }
    }
}
