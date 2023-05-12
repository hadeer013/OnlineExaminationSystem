using exam.BLL.Interfaces;
using exam.DAL.Data;
using exam.DAL.Entities.ExamSubmissionModule;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exam.BLL.Repositories
{
    public class ExamSubmissionRepository : IExamSubmissionRepository
    {
        private readonly ExaminationContext context;

        public ExamSubmissionRepository(ExaminationContext context)
        {
            this.context = context;
        }

        public async Task<ExamSubmission> GetByIdAsync( int ExamCopyId)
        {
           var ExamSub=await context.ExamSubmissions.FindAsync(ExamCopyId);
            return ExamSub;
        }
        public async Task<ExamSubmission> GetByIdIthIncludeAsync(int ExamCopyId)
        {
            var ExamSub=await context.ExamSubmissions.Include(e=>e.QuestionSubmissions).Where(e=>e.ExamCopyId== ExamCopyId).FirstOrDefaultAsync();
            return ExamSub;
        }
        public async Task<ExamSubmission> Add(ExamSubmission examSubmission)
        {
            var entity= context.ExamSubmissions.Add(examSubmission);
            await context.SaveChangesAsync();
            return entity.Entity;
        }
        public async Task<IReadOnlyList<ExamSubmission>> GetUnreviewedExamSubmissions(int examId)
        {
            var result = await context.ExamSubmissions.Where(e=>e.ExamId==examId && e.IsReviewed == false).ToListAsync();
            return result;
        }
    }
}
