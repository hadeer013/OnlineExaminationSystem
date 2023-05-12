using exam.BLL.Services;
using exam.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.BLL.Specification;

namespace exam.BLL.Specifications.SubjectSpec
{
    public class SubjectSpecifSubjectDtoication : BaseSpecification<Subject>
    {

        public SubjectSpecifSubjectDtoication(BaseEntityParams baseEntityParams)
            : base(p => string.IsNullOrEmpty(baseEntityParams.Search) || p.Name.ToLower().Contains(baseEntityParams.Search))
        {
            ApplyPagination(baseEntityParams.PageSize * (baseEntityParams.PageIndex - 1), baseEntityParams.PageSize);
            AddInclude(s => s.Professor);
            AddInclude(s => s.Department);
            AddInclude(s => s.Level);
        }
        public SubjectSpecifSubjectDtoication(int id) : base(s => s.Id == id)
        {
            AddInclude(s => s.Professor);
            AddInclude(s => s.Department);
            AddInclude(s => s.Level);
        }

    }
}
