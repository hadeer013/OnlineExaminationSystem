using exam.DAL.Entities.ExamEntities;

namespace exam.PL.Dtos.AddExamDtos
{
    public class AddExamDto
    {
        public string? ExamName { get; set; }
        public ExamType ExamType { get; set; }
        public DateTime ExamStart { get; set; }
        public DateTime ExamEnd { get; set; }
        public int Duration { get; set; }
        public int SubjectId { get; set; }
        public IEnumerable<AddQuestionTypePointDto> QuestionTypePoints { get; set; }=new HashSet<AddQuestionTypePointDto>();
        public IEnumerable<AddExamQuestionsDistributionDto> examQuestionsDistributions { get; set; } = new HashSet<AddExamQuestionsDistributionDto>();
    }
}
