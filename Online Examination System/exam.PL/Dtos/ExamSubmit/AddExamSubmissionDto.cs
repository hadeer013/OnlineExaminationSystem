using exam.DAL.Entities.ExamSubmissionModule;

namespace exam.PL.Dtos.ExamSubmit
{
    public class AddExamSubmissionDto
    {
        public int ExamCopyId { get; set; }
        public int ExamId { get; set; }
        public ICollection<AddQuestionSubmissionDto> QuestionSubmissions { get; set; }
    }
}

