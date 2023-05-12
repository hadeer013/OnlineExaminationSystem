using exam.DAL.Entities.ExamEntities;

namespace exam.PL.Dtos.AddExamDtos
{
    public class UpdateExamDto
    {
        public int Id { get; set; }
        public string? ExamName { get; set; }
        public ExamType ExamType { get; set; }
        public DateTime ExamStart { get; set; }
        public DateTime ExamEnd { get; set; }
        public int Duration { get; set; }
        public int SubjectId { get; set; }
        public IEnumerable<UpdateQuestionTypePointsDto> QuestionTypePoints { get; set; } = new HashSet<UpdateQuestionTypePointsDto>();
        public IEnumerable<UpdateExamQuestionDistributionDto> examQuestionsDistributions { get; set; } = new HashSet<UpdateExamQuestionDistributionDto>();
    }
}
