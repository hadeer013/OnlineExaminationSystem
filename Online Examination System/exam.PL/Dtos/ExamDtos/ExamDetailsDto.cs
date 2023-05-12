using exam.DAL.Entities.ExamEntities;

namespace exam.PL.Dtos.ExamDtos
{
    public class ExamDetailsDto
    {
        public int Id { get; set; }
        public string? ExamName { get; set; }
        public ExamType ExamType { get; set; }
        public DateTime ExamStart { get; set; }
        public DateTime ExamEnd { get; set; }
        public int Duration { get; set; }
        public float TotalExamPoints { get; set; }
    }
}
