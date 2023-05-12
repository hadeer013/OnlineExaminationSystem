using exam.DAL.Entities.ExamEntities;
using exam.PL.Dtos.ExamDtos;

namespace exam.PL.Dtos
{
    public class BaseExamDto
    {
        public string? ExamName { get; set; }
        public ExamType ExamType { get; set; }
        public DateTime ExamStart { get; set; }
        public DateTime ExamEnd { get; set; }
        public int Duration { get; set; }
        public float TotalExamPoints { get; set; }
        public int SubjectId { get; set; }
        public IEnumerable<QuestionTypePointDto> QuestionTypePoints { get; set; } = new HashSet<QuestionTypePointDto>();
        public IEnumerable<ExamQuestionsDistributionDto> examQuestionsDistributions { get; set; } = new HashSet<ExamQuestionsDistributionDto>();
    }
}
