using AutoMapper;
using exam.BLL.Interfaces;
using exam.BLL.Services;
using exam.BLL.Specifications;
using exam.BLL.Specifications.SubjectSpec;
using exam.DAL.Entities;
using exam.DAL.Entities.ProfessorEntities;
using exam.PL.Dtos;
using exam.PL.Dtos.SubjectDtos;
using exam.PL.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace exam.PL.Controllers
{
    public class SubjectController : BaseApiController
    {
        private readonly ISubjectRepository subjectRepo;
        private readonly IMapper mapper;
        private readonly IGenericRepository<Level> levelRepo;
        private readonly IGenericRepository<Department> departmentRepo;
        private readonly UserManager<ApplicationUser> userManager;

        public SubjectController(ISubjectRepository subjectRepo,IMapper mapper,IGenericRepository<Level> levelRepo,
            IGenericRepository<Department> departmentRepo,UserManager<ApplicationUser> userManager)
        {
            this.subjectRepo = subjectRepo;
            this.mapper = mapper;
            this.levelRepo = levelRepo;
            this.departmentRepo = departmentRepo;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<SubjectDto>>> GetAllSubjects([FromQuery]BaseEntityParams baseEntityParams)
        {
            var spec = new SubjectSpecifSubjectDtoication(baseEntityParams);
            var result = await subjectRepo.GetAllWithSpec(spec);
            var countSpec = new SubjectSpecificationWithFiltersForCountSpec(baseEntityParams);
            var count=await subjectRepo.GetCountAsync(countSpec);
            var subDto = mapper.Map<IReadOnlyList<Subject>, IReadOnlyList<SubjectDto>>(result);
            return Ok(new Pagination<SubjectDto>(baseEntityParams.PageIndex, baseEntityParams.PageSize,count, subDto));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<SubjectDto>> GetSubjectById(int id)
        {
            var spec = new SubjectSpecifSubjectDtoication(id);
            var subject = await subjectRepo.GetByIdWithSpec(spec);
            if (subject == null) return NotFound();
            var subDto = new SubjectDto()
            {
                Id = id,
                DepartmentId = subject.DepartmentId,
                DepartmentName = subject.Department.Name,
                Name = subject.Name,
                LevelId = subject.LevelId,
                LevelName = subject.Level.Name,
                ProfessorId = subject.ProfessorId,
                ProfessorName = subject.Professor.UserName
            };
            return Ok(subDto);
        }

        [HttpPost]//done
        public async Task<ActionResult<Subject>> AddSubject(AddSubjectDto subject)
        {
            var level = await levelRepo.GetByIdAsync(subject.LevelId);
            if (level == null) return NotFound();
            var dep=await departmentRepo.GetByIdAsync(subject.DepartmentId);
            if (dep == null) return NotFound();
            Professor professor;
            if (!string.IsNullOrEmpty(subject.ProfessorId))
            {
                professor = (Professor)await userManager.FindByIdAsync(subject.ProfessorId);
                if (professor == null) return NotFound();
            }
            var dbSubject = new Subject()
            {
                LevelId = level.Id,
                DepartmentId = subject.DepartmentId,
                ProfessorId = subject.ProfessorId,
                Name = subject.Name
            };
            //var mappedSubject = mapper.Map<Subject>(subject);
            var entity= await subjectRepo.Add(dbSubject);

            return Ok(subject);
        }

        [HttpPut]  //Specifies the subjects for each professor //done
        public async Task<ActionResult<Subject>> UpdateSubject(UpdateSubjectDto subject)
        {
            var dbsubject = await subjectRepo.GetByIdAsync(subject.Id);
            if (dbsubject == null) return NotFound();
            var level = await levelRepo.GetByIdAsync(subject.LevelId);
            if (level == null) return NotFound();
            var dep = await departmentRepo.GetByIdAsync(subject.DepartmentId);
            if (dep == null) return NotFound();
            Professor professor;
            if (!string.IsNullOrEmpty(subject.ProfessorId))
            {
                professor = (Professor)await userManager.FindByIdAsync(subject.ProfessorId);
                if(professor == null) return NotFound();
            }
               

            dbsubject.DepartmentId = subject.DepartmentId;
            dbsubject.LevelId = subject.LevelId;
            dbsubject.Name = subject.Name;
            dbsubject.ProfessorId = subject.ProfessorId;
            await subjectRepo.Update(dbsubject);
            return Ok(subject);
        }
        [HttpDelete("{subjectId}")]
        public async Task<ActionResult<Subject>> DeleteSubject(int subjectId)
        {
            var sub = await subjectRepo.GetByIdAsync(subjectId);
            if (sub == null) return NotFound();
            await subjectRepo.Delete(sub);
            return Ok(sub);
        }

        [Authorize(Roles ="Professor,Admin")]
        [HttpGet("getAllSubjectAssignedToProf")]
        public async Task<ActionResult> GetAllSubjectAssignedToProffesor(string? ProffesorId=null)
        {
            string? ProfId = ProffesorId;
            var isProfessor = User.IsInRole("Professor");
            if (isProfessor)
            {
                var Proffesor = await userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));
                ProfId= Proffesor.Id;
            }
            else
            {
                if(string.IsNullOrEmpty(ProffesorId)) return NotFound();
            }
            var subjects = await subjectRepo.GetProfessorSubjectsGroupedByLeval(ProfId);
            return Ok(subjects);
        }

        [Authorize(Roles = "Professor,Admin")]
        [HttpGet("GetAllChaptersWithNumberOfQuestions/{SubjectId}")]
        public async Task<ActionResult> GetAllChaptersWithNumberOfQuestions(int SubjectId)
        {
            var Subject = await subjectRepo.GetByIdAsync(SubjectId);
            if (Subject == null) return NotFound();
            var isProfessor = User.IsInRole("Professor");

            if (isProfessor)
            {
                var Proffesor = await userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));
                if (Subject.ProfessorId != Proffesor.Id) return BadRequest();
            }

            var result=await subjectRepo.GetAllChapterWithNumberOfQuestionsBySubjId(SubjectId);
            return Ok(result);
        }
    }
}
