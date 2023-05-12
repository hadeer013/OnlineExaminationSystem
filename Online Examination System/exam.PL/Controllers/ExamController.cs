using AutoMapper;
using exam.BLL.Interfaces;
using exam.BLL.Specifications.ExamSpecification;
using exam.BLL.Specifications.QuestionsSpecifications;
using exam.DAL.Entities;
using exam.DAL.Entities.ExamEntities;
using exam.DAL.Entities.ExamSubmissionModule;
using exam.DAL.Entities.ExamSubmissionModule.QuestionSumissionTypes;
using exam.DAL.Entities.QuestionsModule;
using exam.DAL.Entities.QuestionsModule.MCQModule;
using exam.PL.Dtos.AddExamDtos;
using exam.PL.Dtos.ExamDtos;
using exam.PL.Dtos.ExamSubmit;
using exam.PL.Dtos.QuestionDtos;
using exam.PL.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace exam.PL.Controllers
{
    public class ExamController : BaseApiController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ISubjectRepository subjectRepository;
        private readonly IMapper mapper;
        private readonly IExamRepository examRepo;
        private readonly IGenericRepository<QuestionTypePoint> questionPointRepo;
        private readonly IExamDistributionRepository distributionRepo;
        private readonly IConfiguration configuration;
        private readonly IGenericRepository<StudentExamCopy> studentEXCopyRepo;
        private readonly IStudentExamCopyWithQuestionRelationhipRepository studentCopyWithQuestionRepo;
        private readonly IExamSubmissionRepository submissionRepo;
        private readonly IGenericRepository<Question> questionRepo;
        private readonly IGenericRepository<QuestionSubmission> questionSubmissionRepo;
        private readonly IMCQwithMultipleCorrectAnsRepository mCQmultiRepo;

        public ExamController(UserManager<ApplicationUser> userManager,ISubjectRepository subjectRepository,IMapper mapper
            , IExamRepository examRepo,IGenericRepository<QuestionTypePoint> questionPointRepo
            , IExamDistributionRepository distributionRepo,IConfiguration configuration,IGenericRepository<StudentExamCopy>studentEXCopyRepo
            , IStudentExamCopyWithQuestionRelationhipRepository StudentCopyWithQuestionRepo,IExamSubmissionRepository submissionRepo,
            IGenericRepository<Question> questionRepo,IGenericRepository<QuestionSubmission> QuestionSubmissionRepo,
            IMCQwithMultipleCorrectAnsRepository MCQmultiRepo)
        {
            this.userManager = userManager;
            this.subjectRepository = subjectRepository;
            this.mapper = mapper;
            this.examRepo = examRepo;
            this.questionPointRepo = questionPointRepo;
            this.distributionRepo = distributionRepo;
            this.configuration = configuration;
            this.studentEXCopyRepo = studentEXCopyRepo;
            studentCopyWithQuestionRepo = StudentCopyWithQuestionRepo;
            this.submissionRepo = submissionRepo;
            this.questionRepo = questionRepo;
            questionSubmissionRepo = QuestionSubmissionRepo;
            mCQmultiRepo = MCQmultiRepo;
        }
        [Authorize(Roles = "Professor,Admin")]
        [HttpPost]
        public async Task<ActionResult> MakeNewExam(AddExamDto examDto)
        {
            var initializer = await userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));
            var subject = await subjectRepository.GetByIdAsync(examDto.SubjectId);
            if (initializer == null) return NotFound();
            var exam = new Exam()
            {
                ExamName = examDto.ExamName,
                Duration = examDto.Duration,
                ExamType = examDto.ExamType,
                SubjectId = examDto.SubjectId,
                ExamEnd = examDto.ExamEnd,
                ExamStart = examDto.ExamStart,
                InitiatorId = initializer.Id
            };
            var entity = await examRepo.Add(exam);
            var Qpoints = mapper.Map<IEnumerable<AddQuestionTypePointDto>, IEnumerable<QuestionTypePoint>>(examDto.QuestionTypePoints);
            foreach (var qpoint in Qpoints)
            {
                qpoint.ExamId = entity.Id;
                await questionPointRepo.Add(qpoint);
            }
            var distributions = mapper.Map<IEnumerable<AddExamQuestionsDistributionDto>, IEnumerable<ExamQuestionsDistribution>>(examDto.examQuestionsDistributions);
            foreach(var distribution in distributions)
            {
                distribution.ExamId = entity.Id;
                await distributionRepo.Add(distribution);
            }
            var ExamTotalPoints = await distributionRepo.GetTotalPoint(entity.Id);
            entity.TotalExamPoints = ExamTotalPoints;
            var updated = await examRepo.Update(entity);

            //ToReturn
            var QpointsDto = mapper.Map<IEnumerable<QuestionTypePoint>, IEnumerable<QuestionTypePointDto>>(updated.QuestionTypePoints);
            var distributionsDto = mapper.Map<IEnumerable<ExamQuestionsDistribution>, IEnumerable<ExamQuestionsDistributionDto>>(updated.examQuestionsDistributions);
            var ExamDto = new ExamPreviewDto()
            {
                Id = updated.Id,
                ExamName = examDto.ExamName,
                Duration = examDto.Duration,
                ExamType = examDto.ExamType,
                SubjectId = examDto.SubjectId,
                ExamEnd = examDto.ExamEnd,
                ExamStart = examDto.ExamStart,
                TotalExamPoints = ExamTotalPoints,
                QuestionTypePoints = QpointsDto,
                examQuestionsDistributions = distributionsDto
            };
            return Ok(ExamDto);
        }

        //updateExam
        [HttpPut("{id}")]
        public async Task<ActionResult<ExamPreviewDto>> UpdateExam([FromQuery]int id, UpdateExamDto updateExamDto)
        {
            var exam = await examRepo.GetByIdAsync(id);
            if (exam == null) return NotFound();
            
            var currentUserEmail =await userManager.FindByEmailAsync( User.FindFirstValue(ClaimTypes.Email));
            if (exam.InitiatorId != currentUserEmail.Id) return Forbid();
            

            exam.ExamName = updateExamDto.ExamName;
            exam.ExamType = updateExamDto.ExamType;
            exam.ExamStart = updateExamDto.ExamStart;
            exam.ExamEnd = updateExamDto.ExamEnd;
            exam.Duration = updateExamDto.Duration;

            var questionTypePoints = mapper.Map<IEnumerable<UpdateQuestionTypePointsDto>, IEnumerable<QuestionTypePoint>>(updateExamDto.QuestionTypePoints);
            exam.QuestionTypePoints = new HashSet<QuestionTypePoint>(questionTypePoints);

            var examQuestionsDistributions = mapper.Map<IEnumerable<UpdateExamQuestionDistributionDto>, IEnumerable<ExamQuestionsDistribution>>(updateExamDto.examQuestionsDistributions);
            exam.examQuestionsDistributions = new HashSet<ExamQuestionsDistribution>(examQuestionsDistributions);

            var updatedExam = await examRepo.Update(exam);

            var examDto = mapper.Map<Exam, ExamPreviewDto>(updatedExam);
            return Ok(examDto);
        }

        //DeleteExam
        [Authorize(Roles = "Professor,Admin")]
        [HttpDelete("{examId}")]
        public async Task<ActionResult> DeleteExam(int examId)
        {
            var exam=await examRepo.GetByIdAsync(examId);
            if (exam == null) return NotFound();
            await examRepo.Delete(exam);
            return Ok();
        }

        [Authorize(Roles ="Student")]
        [HttpGet("takeExam/{examId}")]
        public async Task<ActionResult> TakeExam(int examId)
        {
            var student =(Student) await userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));
            var spec = new ExamSpec(examId);
            var exam = await examRepo.GetByIdWithSpec(spec);
            if (exam == null) return NotFound();
            //if the student is allowed to enter this exam
            if(!(exam.Subject.LevelId== student.LevelId&&exam.Subject.DepartmentId==student.DepartmentId))
                return Forbid();
            if (!(DateTime.Now >= exam.ExamStart && DateTime.Now <= exam.ExamStart.AddMinutes(15)))
                return Forbid();
            //apply the criteria
            List<object> questionList = new List<object>();
            StudentExamCopy ExamCopy = new StudentExamCopy()
            {
                ExamId = examId,
                StudentId = student.Id,
                TakeExamTime = DateTime.Now
            };
            var entity = await studentEXCopyRepo.Add(ExamCopy);
            foreach (var distribution in exam.examQuestionsDistributions)
            {
                var Qs = await examRepo.ApplyExamDistribution(distribution);
                //*****************************************************************************
                foreach (Question question in Qs)
                {
                    QuestionDto questionDto;
                    if (question is MCQwithOneCorrectAns)
                    {
                        MCQwithOneCorrectAns singleAnswerQuestion = (MCQwithOneCorrectAns)question;
                        string[] op =new string[4]{ singleAnswerQuestion.Option1, singleAnswerQuestion.Option2
                                , singleAnswerQuestion.Option3,singleAnswerQuestion.Option4, };
                        Shuffle.shuffle(op);
                        questionDto = new MCQwithOneCorrectAnsDtoForStudent
                        {
                            Id = singleAnswerQuestion.Id,
                            QuestionFormFileUrl = string.IsNullOrEmpty(singleAnswerQuestion.QuestionFormFileUrl) ? null : $"{configuration["BaseUrl"]}{singleAnswerQuestion.QuestionFormFileUrl}",
                            questionContent = singleAnswerQuestion.questionContent,
                            QuestionDifficulty = singleAnswerQuestion.QuestionDifficulty,
                            QuestionType = singleAnswerQuestion.QuestionType,
                            Option1 = op[0],
                            Option2 = op[1],
                            Option3 = op[2],
                            Option4 = op[3]
                        };
                      
                        
                    }
                    else if (question is MCQwithMultipleCorrectAns)
                    {

                        MCQwithMultipleCorrectAns multipleAnswerQuestion = (MCQwithMultipleCorrectAns)question;
                        string[] op = new string[4]{ multipleAnswerQuestion.Option1, multipleAnswerQuestion.Option2
                                , multipleAnswerQuestion.Option3,multipleAnswerQuestion.Option4, };
                        Shuffle.shuffle(op);
                        questionDto = new MCQwithMultipleCorrectAnsDtoForStudent
                        {
                            Id = multipleAnswerQuestion.Id,
                            QuestionFormFileUrl = string.IsNullOrEmpty(multipleAnswerQuestion.QuestionFormFileUrl) ? null : $"{configuration["BaseUrl"]}{multipleAnswerQuestion.QuestionFormFileUrl}",
                            questionContent = multipleAnswerQuestion.questionContent,
                            QuestionDifficulty = multipleAnswerQuestion.QuestionDifficulty,
                            QuestionType = multipleAnswerQuestion.QuestionType,
                            Option1 = op[0],
                            Option2 = op[1],
                            Option3 = op[2],
                            Option4 = op[3]
                        };
                    }
                    else
                    {
                        TrueFalseQuestion trueFalseQuestion = (TrueFalseQuestion)question;

                            questionDto = new QuestionDto
                            {
                                Id = trueFalseQuestion.Id,
                                QuestionFormFileUrl = string.IsNullOrEmpty(trueFalseQuestion.QuestionFormFileUrl) ? null : $"{configuration["BaseUrl"]}{trueFalseQuestion.QuestionFormFileUrl}",
                                questionContent = trueFalseQuestion.questionContent,
                                QuestionDifficulty = trueFalseQuestion.QuestionDifficulty,
                                QuestionType = trueFalseQuestion.QuestionType
                            };
                    }
                    questionList.Add(questionDto);
                    var StudentExamCopyWithQuestionRelationhip = new StudentExamCopyWithQuestionRelationhip()
                    {
                        ExamCopyId = entity.Id,
                        QuestionId = question.Id
                    };
                    await studentCopyWithQuestionRepo.Add(StudentExamCopyWithQuestionRelationhip);
                }
                //*****************************************************************************
            }
            questionList.OrderBy(q => Guid.NewGuid());
            var examDto = new TakeExamDto()
            {
                ExamCopyId = entity.Id,
                ExamName = exam.ExamName,
                ExamStart = exam.ExamStart,
                ExamEnd = exam.ExamEnd,
                ExamType = exam.ExamType,
                QuestionDto = questionList
            };
            return Ok(examDto);
        }
        [Authorize(Roles ="Student")]
        [HttpPost("submitExam")]
        public async Task<ActionResult> SubmitExam(AddExamSubmissionDto examSubmissionDto)
        {
            var student =(Student) await userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));
            var ExamSub = await submissionRepo.GetByIdAsync(examSubmissionDto.ExamCopyId);
            if (ExamSub != null) return BadRequest();

            var spec = new ExamSpec(examSubmissionDto.ExamId);
            var exam = await examRepo.GetByIdWithSpec(spec);
            if (exam == null) return NotFound();
            //if the student is allowed to enter this exam
            if (!(exam.Subject.LevelId == student.LevelId && exam.Subject.DepartmentId == student.DepartmentId))
                return Forbid();
            var midTime= ((exam.ExamEnd - exam.ExamStart).TotalMinutes)/2;
            if (!(DateTime.Now >= exam.ExamStart.AddMinutes(midTime) && DateTime.Now <= exam.ExamEnd))
                return Forbid();
            bool PendResult = false;

            var MCQpoints = exam.QuestionTypePoints.Where(e => e.QuestionType == QuestionTypes.MCQ).FirstOrDefault().Point;
            var TrueFalsepoints = exam.QuestionTypePoints.Where(e => e.QuestionType == QuestionTypes.TrueAndFalse).FirstOrDefault().Point;
            var totalPoints = 0.0f;
            foreach(var QSubmission in examSubmissionDto.QuestionSubmissions)
            {
                var Q = await questionRepo.GetByIdAsync(QSubmission.QuestionId);
                if (Q == null) return BadRequest();
                QuestionSubmission QSub = new QuestionSubmission();
                if (Q is OpenEndedQuestion)
                {
                    PendResult = true;
                    QSubmission.Submission = new OpenEndedSubmission();

                    QSub = new QuestionSubmission()
                    {
                        QuestionId = Q.Id,
                        ExamCopyId = examSubmissionDto.ExamCopyId,
                        QuestionMarkedStatus = QuestionMarkedStatus.Pending,
                        Submission = QSubmission.Submission,
                        QuestionTypes=QuestionTypes.OpenEnded
                    };
                }
                else if (Q is MCQwithMultipleCorrectAns)
                {
                    var Multispec = new MCQquestionSpec(Q.Id);
                    var TrueSubmission = (MCQWithMultipleCorrectSubmission)QSubmission.Submission;
                    var MultiQ = await mCQmultiRepo.GetByIdWithSpec(Multispec);
                    var correctAnswers =  MultiQ.Answers.Select(m => m.Text).ToList();
                    float NumCorrect = await mCQmultiRepo.CheckForCorrectness(correctAnswers, TrueSubmission.Answers);
                    TrueSubmission.CorrectnessPercentage=NumCorrect/ correctAnswers.Count;
                    totalPoints += ((NumCorrect / correctAnswers.Count) * MCQpoints);
                    QSub = new QuestionSubmission()
                    {
                        QuestionId = Q.Id,
                        ExamCopyId = examSubmissionDto.ExamCopyId,
                        QuestionMarkedStatus = QuestionMarkedStatus.Marked,
                        Submission = TrueSubmission,
                        IsCorrect = (NumCorrect > 0),
                        QuestionTypes = QuestionTypes.MCQ
                    };
                   
                }else if (Q is MCQwithOneCorrectAns)
                {
                    //var Multispec = new MCQquestionSpec(Q.Id);
                    var TrueSubmission = (MCQWithOneCorrectSubmission)QSubmission.Submission;
                    //var MCQ = await mCQmultiRepo.GetByIdWithSpec(Multispec);
                    //var correctAnswers =  MultiQ.Answers.Select(m => m.Text).ToList();
                    //float NumCorrect = await mCQmultiRepo.CheckForCorrectness(correctAnswers, TrueSubmission.Answers);
                    //TrueSubmission.CorrectnessPercentage=NumCorrect/ correctAnswers.Count;
                    var MCQ = (MCQwithOneCorrectAns)Q;
                    QSub = new QuestionSubmission()
                    {
                        QuestionId = Q.Id,
                        ExamCopyId = examSubmissionDto.ExamCopyId,
                        QuestionMarkedStatus = QuestionMarkedStatus.Marked,
                        Submission = TrueSubmission,
                        IsCorrect = (MCQ.CorrectAnswer== TrueSubmission.Answer),
                        QuestionTypes = QuestionTypes.MCQ
                    };
                    if(QSub.IsCorrect)
                    totalPoints += MCQpoints;
                }
                else/* if(Q is TrueFalseQuestion)*/
                {
                    //QSubmission.Submission = new TrueAndFalseSubmission();
                    var TrueSubmission = (TrueAndFalseSubmission)QSubmission.Submission;
                    TrueFalseQuestion TrurFalseQ =(TrueFalseQuestion) Q;
                    QSub = new QuestionSubmission()
                    {
                        QuestionId = Q.Id,
                        ExamCopyId = examSubmissionDto.ExamCopyId,
                        QuestionMarkedStatus = QuestionMarkedStatus.Marked,
                        Submission = QSubmission.Submission,
                        IsCorrect= (TrueSubmission.Answer== TrurFalseQ.CorrectAnswer),
                        QuestionTypes = QuestionTypes.TrueAndFalse
                    };
                    if (QSub.IsCorrect)
                        totalPoints += TrueFalsepoints;
                }
                await questionSubmissionRepo.Add(QSub);
            }
            var ExamSubmission = new ExamSubmission()
            {
                StudentId = student.Id,
                ExamCopyId = examSubmissionDto.ExamCopyId,
                ExamId = examSubmissionDto.ExamId,
                ExamType=exam.ExamType,
                TotalMark=totalPoints
            };
            if (!PendResult)//in this case we will show the result to the student directly after submission
            {
                ExamSubmission.IsReviewed = true;
            }
            else //this means that there are openEndedQuestion That needed to reviewed by the professor
            {
                ExamSubmission.IsReviewed = false;
            }
            await submissionRepo.Add(ExamSubmission);
            return Ok(new ExamSubmissionDto()
            {
                ExamCopyId = examSubmissionDto.ExamCopyId,
                TotalMark = totalPoints,
                ExamType = exam.ExamType,
                IsReviewed = ExamSubmission.IsReviewed,
                ExamId = exam.Id
            });
        }
        [Authorize(Roles = "Professor")]
        [HttpGet("viewSubjectExams/{subjectId}")]
        public async Task<ActionResult> ViewSubjectExams(int subjectId)
        {
            var professor = await userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));

            var subject = await subjectRepository.GetByIdAsync(subjectId);
            if (subject == null || subject.ProfessorId != professor.Id)
                return BadRequest();
            var exams = await examRepo.GetExamsBySubjectId(subjectId);
            var result = exams.OrderByDescending(e => e.ExamStart);
            var mapped = mapper.Map<IReadOnlyList<ExamDetailsDto>>(result);
            return Ok(mapped);
        }
        [Authorize(Roles ="Professor")]
        [HttpGet("viewUnreviewedExamCopies/{examId}")]
        public async Task<ActionResult> ViewUnreviewedExamCopies(int examId)
        {
            var professor=await userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));

            var exam = await examRepo.GetByIdAsync(examId);


            var subject=await subjectRepository.GetByIdAsync(exam.SubjectId);
            if (subject == null || subject.ProfessorId != professor.Id)
                return BadRequest();

            var result = await submissionRepo.GetUnreviewedExamSubmissions(examId);
            var mapped = mapper.Map<IReadOnlyList<ExamSubmissionDto>>(result);
            return Ok(mapped);
        }
        [Authorize(Roles ="Professor")]
        [HttpGet("getSpecificUnreviewedExam/{examSubmissionId}")]
        public async Task<ActionResult> getSpecificUnreviewedExam(string StudentId,int ExamCopyId)
        {
            var examSubmission = await submissionRepo.GetByIdIthIncludeAsync(ExamCopyId: ExamCopyId);
            if(examSubmission == null) return NotFound();
            var result=examSubmission.QuestionSubmissions.Where(q=>q.QuestionTypes==QuestionTypes.OpenEnded).ToList();
            foreach(var item in result)
            {
                var ExamSubmissionDto=new AddQuestionSubmissionDto()
                {
                     QuestionId=item.QuestionId,
                      Submission=new OpenEndedSubmission()
                      {
                           Answer=
                      }
                }
            }
        }
        
    }
}
