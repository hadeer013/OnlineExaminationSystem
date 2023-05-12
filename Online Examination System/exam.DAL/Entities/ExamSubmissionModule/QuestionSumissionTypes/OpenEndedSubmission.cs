using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exam.DAL.Entities.ExamSubmissionModule.QuestionSumissionTypes
{
    public class OpenEndedSubmission : IQuestionSubmission
    {
        public string? Answer { get; set; }
    }
}
