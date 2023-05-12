using exam.BLL.Services;
using exam.DAL.Entities.ExamEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.BLL.Specification;

namespace exam.BLL.Specifications.ExamSpecification
{
    public class ExamSpec:BaseSpecification<Exam>
    {
        public ExamSpec(int id) : base(s => s.Id == id)
        {
            
            AddInclude(s => s.QuestionTypePoints);
            AddInclude(s => s.Initiator);
            AddInclude(s => s.Subject);
            AddInclude(s => s.examQuestionsDistributions);
        }
    }
}
