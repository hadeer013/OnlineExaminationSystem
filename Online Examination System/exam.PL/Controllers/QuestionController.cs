using exam.BLL.Interfaces;
using exam.BLL.Specifications.QuestionsSpecifications;
using exam.DAL.Entities.QuestionsModule;
using exam.DAL.Entities.QuestionsModule.MCQModule;
using exam.PL.Dtos.QuestionDtos;
using exam.PL.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace exam.PL.Controllers
{
    public class QuestionController : BaseApiController
    {
        private readonly IGenericRepository<Question> questionRepo;

        public QuestionController(IGenericRepository<Question> QuestionRepo)
        {
            questionRepo = QuestionRepo;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllQuestionsForSubject([FromQuery]QuestionParam questionParam)
        {
            var spec = new QuestionSpec(questionParam);
            var questions = await questionRepo.GetAllWithSpec(spec);
            var countspec = new QuestionsWithFiltersForCountSpec(questionParam);
            var count = await questionRepo.GetCountAsync(countspec);
            return Ok(new Pagination<Question>(questionParam.PageIndex, questionParam.PageSize, count, questions));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetQuestionById(int id)
        {
            var spec = new QuestionSpec(id);
            var question = await questionRepo.GetByIdWithSpec(spec);
            if (question == null) return BadRequest();

            switch (question)
            {
                case MCQwithMultipleCorrectAns mcqMulti:
                    var mcqDto = new MCQwithMultipleCorrectAnsDto
                    {
                        Id = mcqMulti.Id,
                        questionContent = mcqMulti.questionContent,
                        QuestionDifficulty = mcqMulti.QuestionDifficulty,
                        QuestionType = mcqMulti.QuestionType,
                        ChapterName = mcqMulti.Chapter.Name,
                        Options = mcqMulti.Options,
                        Answers = mcqMulti.Answers
                    };
                    return Ok(mcqDto);

                case OpenEndedQuestion openEnded:
                    var openEndedDto = new OpenEndedDto
                    {
                        Id = openEnded.Id,
                        questionContent = openEnded.questionContent,
                        Answer = openEnded.Answer,
                        QuestionDifficulty = openEnded.QuestionDifficulty,
                        QuestionType = QuestionTypes.OpenEnded,
                        ChapterName = openEnded.Chapter.Name
                    };
                    return Ok(openEndedDto);

                case TrueFalseQuestion trueFalse:
                    var trueFalseDto = new TrueFalseQuestionDto
                    {
                        Id = trueFalse.Id,
                        questionContent = trueFalse.questionContent,
                        CorrectAnswer = trueFalse.CorrectAnswer,
                        QuestionDifficulty = trueFalse.QuestionDifficulty,
                        QuestionType = trueFalse.QuestionType,
                        ChapterName = trueFalse.Chapter.Name
                    };
                    return Ok(trueFalseDto);

                default:
                    return BadRequest("Unknown question type");
            }
        }


        [HttpPost]
        public async Task<ActionResult> AddTrueFalseQuestion()
        {

        }
        [HttpPost]
        public async Task<ActionResult> AddTrueFalseQuestion()
        {

        }
        [HttpPost]
        public async Task<ActionResult> AddTrueFalseQuestion()
        {

        }

    }
}
