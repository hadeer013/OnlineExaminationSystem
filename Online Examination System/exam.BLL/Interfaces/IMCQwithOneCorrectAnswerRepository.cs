using exam.DAL.Entities.QuestionsModule.MCQModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exam.BLL.Interfaces
{
    public interface IMCQwithOneCorrectAnswerRepository:IGenericRepository<MCQwithOneCorrectAns>
    {
        Task<bool> ValidateAnswer(List<string> options, string answer);
    }
}
