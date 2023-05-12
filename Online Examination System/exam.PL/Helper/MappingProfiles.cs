using AutoMapper;
using exam.DAL.Entities;
using exam.DAL.Entities.ExamEntities;
using exam.DAL.Entities.ExamSubmissionModule;
using exam.DAL.Entities.ProfessorEntities;
using exam.DAL.Entities.QuestionsModule;
using exam.PL.Dtos;
using exam.PL.Dtos.AddExamDtos;
using exam.PL.Dtos.ExamDtos;
using exam.PL.Dtos.ExamSubmit;
using exam.PL.Dtos.QuestionDtos;
using exam.PL.Helper.Resolvers;

namespace exam.PL.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<AddBaseEntityWithNameDto, Level>();
            CreateMap<AddBaseEntityWithNameDto, Subject>();
            CreateMap<AddBaseEntityWithNameDto, Department>();
            CreateMap<ApplicationUser, BaseApplicationUserDto>();
            CreateMap<SignUpDto, ProfessorSignUpRequests>();
            CreateMap<SignUpDto, BaseApplicationUserDto>();
            CreateMap<ProfessorSignUpRequests, ProfessorRequestsDto>();
            CreateMap<Question, QuestionDto>()
                .ForMember(q => q.QuestionFormFileUrl, o => o.MapFrom<FileUrlResolver>());
            CreateMap<AddExamQuestionsDistributionDto, ExamQuestionsDistribution>();
            CreateMap<ExamQuestionsDistribution, ExamQuestionsDistributionDto>();
            CreateMap<QuestionTypePoint, QuestionTypePointDto>();
            CreateMap<AddQuestionTypePointDto, QuestionTypePoint>();
            CreateMap<Exam, ExamPreviewDto>();
            CreateMap<AnswerDto, AnswerDto>();
            CreateMap<Exam, ExamDetailsDto>();
            CreateMap<ExamSubmission, ExamSubmissionDto>();
        }
    }
}
