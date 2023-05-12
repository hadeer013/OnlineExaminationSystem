using exam.BLL.Interfaces;
using exam.BLL.Services;
using exam.DAL.Data;
using exam.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exam.BLL.Repositories
{
    public class SubjectRepository : GenericRepository<Subject>, ISubjectRepository
    {
        private readonly ExaminationContext context;

        public SubjectRepository(ExaminationContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<SubjectsGroupedByLeval>> GetProfessorSubjectsGroupedByLeval(string ProfessorId)
        {
            var sub = await context.Subjects.Where(p => p.ProfessorId == ProfessorId)
                .GroupBy(S => S.Level).Select(g => new SubjectsGroupedByLeval()
                {
                    Level = g.Key,
                    Subjects = g.Select(s => new SubjectData { Id = s.Id, Name = s.Name })
                }).ToListAsync();
            return sub;

        }
        public async Task<List<ChapterQuestionCountDto>> GetAllChapterWithNumberOfQuestionsBySubjId(int SubjectId)
        {
            var countByChapter = context.Questions.Include(q => q.Chapter)
                                 .Where(q => q.Chapter.SubjectId == SubjectId)
                                 .GroupBy(q => new { q.ChapterId, q.Chapter.Name })
                                 .Select(g => new ChapterQuestionCountDto()
                                 {
                                     ChapterId = g.Key.ChapterId,
                                     ChapterName=g.Key.Name,
                                     QuestionCount = g.Count()
                                 }).ToList();
            return countByChapter;
        }

        public async Task<IReadOnlyList<Subject>> GetsSubjectsForStudent(int LevelId,int DepartmentId)
        {
            var sub = await context.Subjects.Where(s => s.DepartmentId == DepartmentId && s.LevelId == LevelId)
                .ToListAsync();
            return sub;
        }
    }
}
