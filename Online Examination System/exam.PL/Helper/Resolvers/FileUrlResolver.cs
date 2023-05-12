using AutoMapper;
using exam.DAL.Entities.QuestionsModule;
using exam.PL.Dtos.QuestionDtos;
using Microsoft.Extensions.Configuration;


namespace exam.PL.Helper.Resolvers
{
    public class FileUrlResolver : IValueResolver<Question, QuestionDto, string>
    {
        private readonly IConfiguration configuration;

        public FileUrlResolver(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string Resolve(Question source, QuestionDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.QuestionFormFileUrl))
            {
                return $"{configuration["BaseUrl"]}{source.QuestionFormFileUrl}";
            }
            return null;
        }
    }
}
