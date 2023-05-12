using exam.DAL.Entities.ExamEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exam.BLL.Interfaces
{
    public interface IStudentExamCopyWithQuestionRelationhipRepository
    {
        Task<StudentExamCopyWithQuestionRelationhip> Add(StudentExamCopyWithQuestionRelationhip entity);
    }
}
