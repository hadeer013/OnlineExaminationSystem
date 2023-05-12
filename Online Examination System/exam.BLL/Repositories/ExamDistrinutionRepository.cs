using exam.BLL.Interfaces;
using exam.BLL.Services;
using exam.DAL.Data;
using exam.DAL.Entities.ExamEntities;
using exam.DAL.Entities.QuestionsModule;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exam.BLL.Repositories
{
    public class ExamDistrinutionRepository : GenericRepository<ExamQuestionsDistribution>, IExamDistributionRepository
    {
        private readonly ExaminationContext context;

        public ExamDistrinutionRepository(ExaminationContext context) : base(context)
        {
            this.context = context;
        }

        private async Task<IEnumerable<QuestionTypeWithCount>> QuestionTypesCount(int examId)
        {
            var examQuestionsDistributions = await context.ExamQuestionsDistributions
                                       .Where(e => e.ExamId == examId)
                                       .GroupBy(e => e.QuestionType)
                                       .Select(g => new QuestionTypeWithCount()
                                       {
                                           QuestionType = g.Key,
                                           QuestionCount = g.Sum(e => e.NumOfQuestion)
                                       }).ToListAsync();
            return examQuestionsDistributions;
        }
        public async Task<float> GetTotalPoint(int examId)
        {
            float totalPoints = 0.0f;
            var result = await QuestionTypesCount(examId);
            var total = context.QuestionTypePoints.Where(q => q.ExamId == examId)
                .Join(result, q => q.QuestionType, p => p.QuestionType,
                (q, p) => new { QuestionType = p.QuestionType, QPoints = q.Point * p.QuestionCount }
                );
            foreach(var t in total)
                totalPoints+= t.QPoints;
            return totalPoints;
        }
    }
}
