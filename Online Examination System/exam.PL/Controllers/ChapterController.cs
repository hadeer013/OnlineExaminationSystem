using AutoMapper;
using exam.BLL.Interfaces;
using exam.DAL.Entities;
using exam.PL.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace exam.PL.Controllers
{
    public class ChapterController : BaseApiController
    {
        private readonly IGenericRepository<Chapter> chapterRepo;
        private readonly IMapper mapper;

        public ChapterController(IGenericRepository<Chapter> chapterRepo,IMapper mapper)
        {
            this.chapterRepo = chapterRepo;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Chapter>>> GetAllChapters()
        {
            var result = await chapterRepo.GetAllAsync();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Chapter>> GetChapterById(int id)
        {
            var chapter = await chapterRepo.GetByIdAsync(id);
            if (chapter == null) return NotFound();
            return Ok(chapter);
        }

        [HttpPost]
        public async Task<ActionResult<Chapter>> AddChapter(AddBaseEntityWithNameDto chapter)
        {
            var mappedChapter= mapper.Map<Chapter>(chapter);
            await chapterRepo.Add(mappedChapter);
            return Ok(mappedChapter);
        }

        [HttpPut]
        public async Task<ActionResult<Chapter>> UpdateLevel(UpdateChapterDto chapter)
        {
            var ch = await chapterRepo.GetByIdAsync(chapter.Id);
            ch.Name = chapter.Name;
            await chapterRepo.Update(ch);
            return Ok(chapter);
        }
        [HttpDelete]
        public async Task<ActionResult<Chapter>> DeleteChapter(int chapterId)
        {
            var chapter = await chapterRepo.GetByIdAsync(chapterId);
            if (chapter == null) return NotFound();
            await chapterRepo.Delete(chapter);
            return Ok(chapter);
        }
    }
}
