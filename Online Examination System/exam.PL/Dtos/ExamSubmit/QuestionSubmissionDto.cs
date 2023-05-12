using exam.DAL.Entities.ExamSubmissionModule;

namespace exam.PL.Dtos.ExamSubmit
{
    public class QuestionSubmissionDto
    {
        public int QuestionSubmissionId { get; set; }
        public int QuestionId { get; set; }
        public int ExamCopyId { get; set; }
        public bool IsCorrect { get; set; }
        public IQuestionSubmission Submission { get; set; }
        public QuestionMarkedStatus QuestionMarkedStatus { get; set; } = QuestionMarkedStatus.Pending;

    }
}
