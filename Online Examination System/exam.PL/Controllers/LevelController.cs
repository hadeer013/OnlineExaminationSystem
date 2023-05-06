using AutoMapper;
using exam.BLL.Interfaces;
using exam.DAL.Entities;
using exam.PL.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace exam.PL.Controllers
{
    public class LevelController : BaseApiController
    {
        private readonly IGenericRepository<Level> levelRepo;
        private readonly IMapper mapper;

        public LevelController(IGenericRepository<Level> levelRepo,IMapper mapper)
        {
            this.levelRepo = levelRepo;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Level>>> GetAllLevels()
        {
            var result = await levelRepo.GetAllAsync();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Level>> GetLevelById(int id)
        {
            var level = await levelRepo.GetByIdAsync(id);
            if (level == null) return NotFound();
            return Ok(level);
        }

        [HttpPost]
        public async Task<ActionResult<Level>> AddLevel(AddBaseEntityWithNameDto level)
        {
            var mappedLevel = mapper.Map<Level>(level);
            await levelRepo.Add(mappedLevel);
            return Ok(mappedLevel);
        }

        [HttpPut]
        public async Task<ActionResult<Level>> UpdateLevel(Level level)
        {
            await levelRepo.Update(level);
            return Ok(level);
        }
        [HttpDelete]
        public async Task<ActionResult<Level>> DeleteLevel(int levelId)
        {
            var level = await levelRepo.GetByIdAsync(levelId);
            if (level == null) return NotFound();
            await levelRepo.Delete(level);
            return Ok(level);
        }
    }
}
