using AutoMapper;
using exam.BLL.Interfaces;
using exam.DAL.Entities;
using exam.PL.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace exam.PL.Controllers
{
    public class SubjectController : BaseApiController
    {
        private readonly IGenericRepository<Subject> subjectRepo;
        private readonly IMapper mapper;

        public SubjectController(IGenericRepository<Subject> subjectRepo,IMapper mapper)
        {
            this.subjectRepo = subjectRepo;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Subject>>> GetAllSubjects()
        {
            var result = await subjectRepo.GetAllAsync();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Subject>> GetSubjectById(int id)
        {
            var subject = await subjectRepo.GetByIdAsync(id);
            if (subject == null) return NotFound();
            return Ok(subject);
        }

        [HttpPost]
        public async Task<ActionResult<Subject>> AddSubject(AddBaseEntityWithNameDto subject)
        {
            var mappedSubject = mapper.Map<Subject>(subject);
            await subjectRepo.Add(mappedSubject);
            return Ok(mappedSubject);
        }

        [HttpPut]
        public async Task<ActionResult<Subject>> UpdateSubject(Subject subject)
        {
            await subjectRepo.Update(subject);
            return Ok(subject);
        }
        [HttpDelete]
        public async Task<ActionResult<Subject>> DeleteSubject(int subjectId)
        {
            var sub = await subjectRepo.GetByIdAsync(subjectId);
            if (sub == null) return NotFound();
            await subjectRepo.Delete(sub);
            return Ok(sub);
        }
    }
}
