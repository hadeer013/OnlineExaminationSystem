using exam.BLL.Interfaces;
using exam.DAL.Entities.ExamSubmissionModule;

namespace exam.PL.Dtos.ExamSubmit
{
    public class AddQuestionSubmissionDto
    {
        public int QuestionId { get; set; }
        public IQuestionSubmission Submission { get; set; }
    }
}
