using exam.DAL.Entities.QuestionsModule.MCQModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.BLL.Specification;

namespace exam.BLL.Specifications.QuestionsSpecifications
{
    public class MCQquestionSpec : BaseSpecification<MCQwithMultipleCorrectAns>
    {
        public MCQquestionSpec(QuestionParam questionParams)
           : base(p => ((questionParams.QustionDifficulty == null) || (questionParams.QustionDifficulty == p.QuestionDifficulty))
           && ((questionParams.QuestionTypes == null) || (questionParams.QuestionTypes == p.QuestionType))
           && ((questionParams.ChapterId == null) || p.ChapterId == questionParams.ChapterId)
         )
        {
            AddInclude(q => q.Answers);
        }
        public MCQquestionSpec(int Qid) : base((q) => q.Id == Qid)
        {
            AddInclude(q => q.Answers);
        }
    }
}
