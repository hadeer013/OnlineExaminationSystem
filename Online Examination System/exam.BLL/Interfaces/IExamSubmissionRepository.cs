using exam.DAL.Entities.ExamSubmissionModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exam.BLL.Interfaces
{
    public interface IExamSubmissionRepository
    {
        Task<ExamSubmission> GetByIdAsync(int ExamCopyId);
        Task<ExamSubmission> Add(ExamSubmission examSubmission);
        Task<IReadOnlyList<ExamSubmission>> GetUnreviewedExamSubmissions(int examId);
        Task<ExamSubmission> GetByIdIthIncludeAsync(int ExamCopyId);


    }
}
