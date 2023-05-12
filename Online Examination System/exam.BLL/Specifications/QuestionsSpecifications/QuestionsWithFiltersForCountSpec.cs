using exam.DAL.Entities.QuestionsModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.BLL.Specification;

namespace exam.BLL.Specifications.QuestionsSpecifications
{
    public class QuestionsWithFiltersForCountSpec : BaseSpecification<Question>
    {
        public QuestionsWithFiltersForCountSpec(QuestionParam questionParams)
           : base(p => ((questionParams.QustionDifficulty == null) || (questionParams.QustionDifficulty == p.QuestionDifficulty))
           && ((questionParams.QuestionTypes == null) || (questionParams.QuestionTypes == p.QuestionType))
           && ((questionParams.ChapterId == null) || p.ChapterId == questionParams.ChapterId)
         )
        {
            AddInclude(q => q.Chapter);
        }
    }
}
