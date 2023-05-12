using exam.BLL.Services;
using exam.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.BLL.Specification;

namespace exam.BLL.Specifications.SubjectSpec
{
    public class SubjectSpecificationWithFiltersForCountSpec:BaseSpecification<Subject>
    {
        public SubjectSpecificationWithFiltersForCountSpec(BaseEntityParams baseEntityParams)
            : base(p => string.IsNullOrEmpty(baseEntityParams.Search) || p.Name.ToLower().Contains(baseEntityParams.Search))
        { }
    }
}
