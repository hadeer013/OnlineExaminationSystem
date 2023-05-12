using exam.DAL.Entities.QuestionsModule.MCQModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exam.BLL.Interfaces
{
    public interface IMCQwithMultipleCorrectAnsRepository:IGenericRepository<MCQwithMultipleCorrectAns>
    {
        Task<bool> ValidateAnswers(List<string> options, List<string> answers);
        Task<int> CheckForCorrectness(List<string> CorrectAnswers, List<string> SubmitAnswers);
    }
}
