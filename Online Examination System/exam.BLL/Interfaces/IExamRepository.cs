using exam.DAL.Entities.ExamEntities;
using exam.DAL.Entities.QuestionsModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exam.BLL.Interfaces
{
    public interface IExamRepository:IGenericRepository<Exam>
    {
         Task<IReadOnlyList<Question>> ApplyExamDistribution(ExamQuestionsDistribution questionsDistribution);
        Task<IReadOnlyList<Exam>> GetExamsBySubjectId(int SubjectId);
    }
}
