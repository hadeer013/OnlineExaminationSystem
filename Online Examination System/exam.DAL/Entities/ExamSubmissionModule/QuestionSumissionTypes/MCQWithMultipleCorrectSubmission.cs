using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exam.DAL.Entities.ExamSubmissionModule.QuestionSumissionTypes
{
    public class MCQWithMultipleCorrectSubmission:IQuestionSubmission
    {
        public List<string> Answers { get; set; }
        public float CorrectnessPercentage { get; set; }
    }
}
