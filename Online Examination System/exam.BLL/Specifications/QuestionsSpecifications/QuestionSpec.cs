using exam.DAL.Entities;
using exam.DAL.Entities.QuestionsModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.BLL.Specification;
using VHW.BLL.Specification;

namespace exam.BLL.Specifications.QuestionsSpecifications
{
    public class QuestionSpec: BaseSpecification<Question>
    {
        public QuestionSpec(QuestionParam questionParams)
          : base(p => ((questionParams.QustionDifficulty==null) || (questionParams.QustionDifficulty==p.QuestionDifficulty))
          &&((questionParams.QuestionTypes == null) || (questionParams.QuestionTypes == p.QuestionType))
          &&(p.Chapter.SubjectId==questionParams.SubjectId) &&((questionParams.ChapterId == null) || p.ChapterId== questionParams.ChapterId)
                 )
        {
            AddInclude(q => q.Chapter);
            ApplyPagination((questionParams.PageSize * (questionParams.PageIndex - 1)), questionParams.PageSize);

        }
        public QuestionSpec(int id) : base((q) => q.Id == id)
        {

        }
    }

}
