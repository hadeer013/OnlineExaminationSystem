using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exam.DAL.Entities.ExamSubmissionModule.QuestionSumissionTypes
{
    public class TrueAndFalseSubmission:IQuestionSubmission
    {
        public bool Answer { get; set; }
    }
}
