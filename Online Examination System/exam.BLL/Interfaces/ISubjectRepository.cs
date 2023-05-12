using exam.BLL.Services;
using exam.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exam.BLL.Interfaces
{
    public interface ISubjectRepository:IGenericRepository<Subject>
    {
        Task<IEnumerable<SubjectsGroupedByLeval>> GetProfessorSubjectsGroupedByLeval(string ProfessorId);
        Task<List<ChapterQuestionCountDto>> GetAllChapterWithNumberOfQuestionsBySubjId(int SubjectId);
        Task<IReadOnlyList<Subject>> GetsSubjectsForStudent(int LevelId, int DepartmentId);
    }
}
