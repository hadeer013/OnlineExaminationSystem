using AutoMapper;
using exam.BLL.Interfaces;
using exam.BLL.Specifications.QuestionsSpecifications;
using exam.DAL.Entities;
using exam.DAL.Entities.QuestionsModule;
using exam.DAL.Entities.QuestionsModule.MCQModule;
using exam.PL.Document;
using exam.PL.Dtos;
using exam.PL.Dtos.QuestionAdditionDto;
using exam.PL.Dtos.QuestionDtos;
using exam.PL.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace exam.PL.Controllers
{
    public class QuestionController : BaseApiController
    {
        private readonly IGenericRepository<Question> questionRepo;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;
        private readonly IGenericRepository<Chapter> chapterRepo;
        private readonly IMCQwithMultipleCorrectAnsRepository multiQRepo;
        private readonly IGenericRepository<Answer> answerRepo;
        private readonly IMCQwithOneCorrectAnswerRepository oneQRepository;

        public QuestionController(IGenericRepository<Question> QuestionRepo,IMapper mapper
            ,IConfiguration configuration, IGenericRepository<Chapter> chapterRepo, IMCQwithMultipleCorrectAnsRepository MultiQRepo,
            IGenericRepository<Answer>answerRepo,IMCQwithOneCorrectAnswerRepository OneQRepository)
        {
            questionRepo = QuestionRepo;
            this.mapper = mapper;
            this.configuration = configuration;
            this.chapterRepo = chapterRepo;
            multiQRepo = MultiQRepo;
            this.answerRepo = answerRepo;
            oneQRepository = OneQRepository;
        }

        [Authorize] 
        [HttpGet]
        public async Task<ActionResult <Pagination<QuestionDto>>> GetAllQuestionsForSubject([FromQuery]QuestionParam questionParam)
        {
            var IsStudent = User.IsInRole("Student");
            var spec = new QuestionSpec(questionParam);
            var questions = await questionRepo.GetAllWithSpec(spec);
            var countspec = new QuestionsWithFiltersForCountSpec(questionParam);
            var count = await questionRepo.GetCountAsync(countspec);
            //******************************************************************

            List<QuestionDto> questionDtos = new List<QuestionDto>();
            foreach (Question question in questions)
            {
                QuestionDto questionDto;
                if (question is MCQwithOneCorrectAns)
                {
                    MCQwithOneCorrectAns singleAnswerQuestion = (MCQwithOneCorrectAns)question;

                    if (IsStudent)
                    {
                        questionDto = new MCQwithOneCorrectAnsDtoForStudent
                        {
                            Id = singleAnswerQuestion.Id,
                            QuestionFormFileUrl = string.IsNullOrEmpty(singleAnswerQuestion.QuestionFormFileUrl) ? null : $"{configuration["BaseUrl"]}{singleAnswerQuestion.QuestionFormFileUrl}",
                            questionContent = singleAnswerQuestion.questionContent,
                            QuestionDifficulty = singleAnswerQuestion.QuestionDifficulty,
                            QuestionType = singleAnswerQuestion.QuestionType,
                            Option1 = singleAnswerQuestion.Option1,
                            Option2 = singleAnswerQuestion.Option2,
                            Option3 = singleAnswerQuestion.Option3,
                            Option4 = singleAnswerQuestion.Option4,
                        };
                    }
                    else
                    {
                        questionDto = new MCQwithOneCorrectAnsDto
                        {
                            Id = singleAnswerQuestion.Id,
                            QuestionFormFileUrl = string.IsNullOrEmpty(singleAnswerQuestion.QuestionFormFileUrl) ? null : $"{configuration["BaseUrl"]}{singleAnswerQuestion.QuestionFormFileUrl}",
                            questionContent = singleAnswerQuestion.questionContent,
                            QuestionDifficulty = singleAnswerQuestion.QuestionDifficulty,
                            QuestionType = singleAnswerQuestion.QuestionType,
                            Option1 = singleAnswerQuestion.Option1,
                            Option2 = singleAnswerQuestion.Option2,
                            Option3 = singleAnswerQuestion.Option3,
                            Option4 = singleAnswerQuestion.Option4,
                            Answer = singleAnswerQuestion.CorrectAnswer
                        };
                    }
                }
                else if (question is MCQwithMultipleCorrectAns)
                {
                    
                    MCQwithMultipleCorrectAns multipleAnswerQuestion = (MCQwithMultipleCorrectAns)question;

                    if (IsStudent)
                    {
                        questionDto = new MCQwithMultipleCorrectAnsDtoForStudent
                        {
                            Id = multipleAnswerQuestion.Id,
                            QuestionFormFileUrl = string.IsNullOrEmpty(multipleAnswerQuestion.QuestionFormFileUrl) ? null : $"{configuration["BaseUrl"]}{multipleAnswerQuestion.QuestionFormFileUrl}",
                            questionContent = multipleAnswerQuestion.questionContent,
                            QuestionDifficulty = multipleAnswerQuestion.QuestionDifficulty,
                            QuestionType = multipleAnswerQuestion.QuestionType,
                            Option1 = multipleAnswerQuestion.Option1,
                            Option2 = multipleAnswerQuestion.Option2,
                            Option3 = multipleAnswerQuestion.Option3,
                            Option4 = multipleAnswerQuestion.Option4,
                        };
                    }
                    else
                    {
                        var Multiplespec = new MCQquestionSpec(multipleAnswerQuestion.Id);
                        var MutliQuestion = await multiQRepo.GetByIdWithSpec(Multiplespec);
                        var ansDt = mapper.Map<IEnumerable<Answer>, IEnumerable<AnswerDto>>(MutliQuestion.Answers);
                        questionDto = new MCQwithMultipleCorrectAnsDto
                        {
                            Id = multipleAnswerQuestion.Id,
                            QuestionFormFileUrl = string.IsNullOrEmpty(multipleAnswerQuestion.QuestionFormFileUrl) ? null : $"{configuration["BaseUrl"]}{multipleAnswerQuestion.QuestionFormFileUrl}",
                            questionContent = multipleAnswerQuestion.questionContent,
                            QuestionDifficulty = multipleAnswerQuestion.QuestionDifficulty,
                            QuestionType = multipleAnswerQuestion.QuestionType,
                            Option1 = multipleAnswerQuestion.Option1,
                            Option2 = multipleAnswerQuestion.Option2,
                            Option3 = multipleAnswerQuestion.Option3,
                            Option4 = multipleAnswerQuestion.Option4,
                            Answers = ansDt
                        };
                    }
                }
                else 
                {
                    TrueFalseQuestion trueFalseQuestion = (TrueFalseQuestion)question;

                    if (IsStudent)
                    {
                        questionDto = new QuestionDto
                        {
                            Id = trueFalseQuestion.Id,
                            QuestionFormFileUrl = string.IsNullOrEmpty(trueFalseQuestion.QuestionFormFileUrl) ? null : $"{configuration["BaseUrl"]}{trueFalseQuestion.QuestionFormFileUrl}",
                            questionContent = trueFalseQuestion.questionContent,
                            QuestionDifficulty = trueFalseQuestion.QuestionDifficulty,
                            QuestionType = trueFalseQuestion.QuestionType
                        };
                    }
                    else
                    {
                        questionDto = new TrueFalseQuestionDto
                        {
                            Id = trueFalseQuestion.Id,
                            QuestionFormFileUrl = $"{configuration["BaseUrl"]}{trueFalseQuestion.QuestionFormFileUrl}" ,
                            QuestionDifficulty = trueFalseQuestion.QuestionDifficulty,
                            QuestionType = trueFalseQuestion.QuestionType,
                            Answer = trueFalseQuestion.CorrectAnswer
                        };
                    }
                }
                questionDtos.Add(questionDto);
            }
            return Ok(new Pagination<object>(questionParam.PageIndex, questionParam.PageSize, count, questionDtos));
        }


        [Authorize(Roles = "Professor,Admin")]
        [HttpPost("AddTrueFalseQuestion")]
        public async Task<ActionResult> AddTrueFalseQuestion([FromForm]AddTrueFalseQuesDto quesDto)
        {
            var chapter = await chapterRepo.GetByIdAsync(quesDto.ChapterId);
            if (chapter == null) return BadRequest();
            string FileUrl = null;
            if (quesDto.QuestionFormFile != null)
               FileUrl = DocumetSettings.UploadFile(quesDto.QuestionFormFile, "QuestionFiles");
            var TrueFalseQuestion = new TrueFalseQuestion()
            {
                QuestionFormFileUrl = FileUrl,
                ChapterId = chapter.Id,
                questionContent = quesDto.questionContent,
                CorrectAnswer = quesDto.Answer,
                QuestionDifficulty = quesDto.QuestionDifficulty
            };
            await questionRepo.Add(TrueFalseQuestion);
            var mapped = mapper.Map<TrueFalseQuestion, TrueFalseQuestionDto>(TrueFalseQuestion);
            return Ok(mapped);
        }
        [Authorize(Roles = "Professor,Admin")]
        [HttpPost("AddOpenEndedQuestion")]
        public async Task<ActionResult> AddOpenEndedQuestion([FromForm] AddQuestionDto AddquestionDto)
        {
            var chapter = await chapterRepo.GetByIdAsync(AddquestionDto.ChapterId);
            if (chapter == null) return BadRequest();
            string FileUrl = null;
            if (AddquestionDto.QuestionFormFile != null)
                FileUrl = DocumetSettings.UploadFile(AddquestionDto.QuestionFormFile, "QuestionFiles");
            var OpenEndedQuestion = new OpenEndedQuestion()
            {
                QuestionFormFileUrl = FileUrl,
                ChapterId = chapter.Id,
                questionContent = AddquestionDto.questionContent,
                QuestionDifficulty = AddquestionDto.QuestionDifficulty
            };
            await questionRepo.Add(OpenEndedQuestion);
            var mapped = mapper.Map<OpenEndedQuestion, QuestionDto>(OpenEndedQuestion);
            return Ok(mapped);
        }
        [Authorize(Roles = "Professor,Admin")]
        [HttpPost("AddMCQwithOneAnswerQuestion")]
        public async Task<ActionResult> AddMCQwithOneAnswerQuestion([FromForm] AddMCQwithOneAnswerDto mCQwithOneAnswerDto)
        {

            var chapter = await chapterRepo.GetByIdAsync(mCQwithOneAnswerDto.ChapterId);
            if (chapter == null) return BadRequest();
            if (!await oneQRepository.ValidateAnswer(mCQwithOneAnswerDto.Options, mCQwithOneAnswerDto.CorrectAnswer)) return BadRequest();
            string FileUrl = null;
            if (mCQwithOneAnswerDto.QuestionFormFile != null)
                FileUrl = DocumetSettings.UploadFile(mCQwithOneAnswerDto.QuestionFormFile, "QuestionFiles");
            var MCQwithOneCorrectAns = new MCQwithOneCorrectAns()
            {
                QuestionFormFileUrl = FileUrl,
                ChapterId = chapter.Id,
                questionContent = mCQwithOneAnswerDto.questionContent,
                QuestionDifficulty = mCQwithOneAnswerDto.QuestionDifficulty,
                Option1 = mCQwithOneAnswerDto.Options[0],
                Option2 = mCQwithOneAnswerDto.Options[1],
                Option3 = mCQwithOneAnswerDto.Options[2],
                Option4 = mCQwithOneAnswerDto.Options[3],
                CorrectAnswer = mCQwithOneAnswerDto.CorrectAnswer
            };
            await questionRepo.Add(MCQwithOneCorrectAns);
            var mapped = mapper.Map<MCQwithOneCorrectAns, MCQwithOneCorrectAnsDto>(MCQwithOneCorrectAns);
            return Ok(mapped);
        }
        [Authorize(Roles ="Professor,Admin")]
        [HttpPost("AddMCQwithMultipleAnswersQuestion")]
        public async Task<ActionResult> AddMCQwithMultipleAnswersQuestion([FromForm] AddMCQwithMultipleAnswers mCQwithMultipleAnswers)
        {
            var chapter = await chapterRepo.GetByIdAsync(mCQwithMultipleAnswers.ChapterId);
            if (chapter == null) return BadRequest();
            if (!await multiQRepo.ValidateAnswers(mCQwithMultipleAnswers.Options, mCQwithMultipleAnswers.Answers)) return BadRequest();
            string FileUrl = null;
            if (mCQwithMultipleAnswers.QuestionFormFile != null)
                FileUrl = DocumetSettings.UploadFile(mCQwithMultipleAnswers.QuestionFormFile, "QuestionFiles");
            var ans = mapper.Map<List<string>, List<Answer>>(mCQwithMultipleAnswers.Answers);
            
            var MCQwithOneCorrectAns = new MCQwithMultipleCorrectAns()
            {
                QuestionFormFileUrl = FileUrl,
                ChapterId = chapter.Id,
                questionContent = mCQwithMultipleAnswers.questionContent,
                QuestionDifficulty = mCQwithMultipleAnswers.QuestionDifficulty,
                Option1 = mCQwithMultipleAnswers.Options[0],
                Option2 = mCQwithMultipleAnswers.Options[1],
                Option3 = mCQwithMultipleAnswers.Options[2],
                Option4 = mCQwithMultipleAnswers.Options[3]
            };
            var entity= await questionRepo.Add(MCQwithOneCorrectAns);
            foreach (var an in mCQwithMultipleAnswers.Answers)
            {
                var DbAns = new Answer() { Text = an , QuestionId=entity.Id};
                await answerRepo.Add(DbAns);
            }
            var mapped = mapper.Map<MCQwithMultipleCorrectAns, MCQwithMultipleCorrectAnsDto>(MCQwithOneCorrectAns);
            return Ok(mapped);
        }

        
    }
}
