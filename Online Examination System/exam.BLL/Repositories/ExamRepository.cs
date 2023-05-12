using exam.BLL.Interfaces;
using exam.DAL.Data;
using exam.DAL.Entities;
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
    public class ExamRepository : GenericRepository<Exam>, IExamRepository
    {
        private readonly ExaminationContext context;

        public ExamRepository(ExaminationContext context) : base(context)
        {
            this.context = context;
        }
        public async Task<IReadOnlyList<Question>> ApplyExamDistribution(ExamQuestionsDistribution questionsDistribution)
        {
            var Questions = await context.Questions
                .Where(q => q.ChapterId == questionsDistribution.ChapterId && q.QuestionDifficulty == questionsDistribution.qustionDifficulty && q.QuestionType == questionsDistribution.QuestionType)
                .OrderBy(q => Guid.NewGuid()) // shuffle the questions randomly
                .Take(questionsDistribution.NumOfQuestion)
                .ToListAsync();
            return Questions;
        }
        public async Task<IReadOnlyList<Exam>> GetExamsBySubjectId(int SubjectId)
        {
            var Q = await context.Exams.Where(e => e.SubjectId == SubjectId).ToListAsync();
            return Q;
        }
    }
}
