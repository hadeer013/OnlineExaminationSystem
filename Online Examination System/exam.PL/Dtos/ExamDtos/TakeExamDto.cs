using exam.DAL.Entities.ExamEntities;

namespace exam.PL.Dtos.ExamDtos
{
    public class TakeExamDto
    {
        public int ExamCopyId { get; set; }
        public string ExamName { get; set;}
        public string SubjectName { get; set; }
        public ExamType ExamType { get; set; }
        public DateTime ExamStart { get; set; }
        public DateTime ExamEnd { get; set; }
        public IEnumerable<object>QuestionDto { get; set; }

    }
}
