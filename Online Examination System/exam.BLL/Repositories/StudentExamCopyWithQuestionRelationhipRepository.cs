using exam.DAL.Data;
using exam.DAL.Entities.ExamEntities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exam.BLL.Repositories
{
    public class StudentExamCopyWithQuestionRelationhipRepository
    {
        private readonly ExaminationContext context;

        public StudentExamCopyWithQuestionRelationhipRepository(ExaminationContext context) 
        {
            this.context = context;
        }
        public async Task<StudentExamCopyWithQuestionRelationhip> Add(StudentExamCopyWithQuestionRelationhip entity)
        {
            var ent= context.Add(entity);
            await context.SaveChangesAsync();
            return ent.Entity;
        }
    }
}
