using exam.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exam.BLL.Services
{
    public class SubjectsGroupedByLeval
    {
        public Level? Level { get; set; }
        public IEnumerable<SubjectData>? Subjects { get; set; }
    }
}
