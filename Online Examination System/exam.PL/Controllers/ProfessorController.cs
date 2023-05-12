using AutoMapper;
using exam.BLL.Interfaces;
using exam.BLL.Repositories;
using exam.DAL.Entities;
using exam.DAL.Entities.ProfessorEntities;
using exam.PL.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace exam.PL.Controllers
{
    public class ProfessorController : BaseApiController
    {
        private readonly IUserGenericRepository<Professor> professorRepo;
        private readonly IGenericRepository<ProfessorSignUpRequests> profSignupRequestRepo;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;

        public ProfessorController(IUserGenericRepository<Professor> professorRepo,IGenericRepository<ProfessorSignUpRequests> profSignupRequestRepo,UserManager<ApplicationUser> userManager,IMapper mapper)
        {
            this.professorRepo = professorRepo;
            this.profSignupRequestRepo = profSignupRequestRepo;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        [Authorize(Roles ="Admin")]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<BaseApplicationUserDto>>> GetAllProfessors()
        {
            var result = await professorRepo.GetAllAsync();
            var mapped = mapper.Map<IReadOnlyList<BaseApplicationUserDto>>(result);
            return Ok(mapped);
        }
        [HttpGet("{Id}")]
        public async Task<ActionResult<BaseApplicationUserDto>> GetProfessorById(string Id)
        {
            var user = await userManager.FindByIdAsync(Id);
            if (user == null) return NotFound();
            var mapped = mapper.Map<BaseApplicationUserDto>(user);
            return Ok(mapped);
        }

    }
}
