using exam.BLL.Services;
using exam.DAL.Entities.ExamEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exam.BLL.Interfaces
{
    public interface IExamDistributionRepository:IGenericRepository<ExamQuestionsDistribution>
    {
        Task<float> GetTotalPoint(int examId);
    }
}
