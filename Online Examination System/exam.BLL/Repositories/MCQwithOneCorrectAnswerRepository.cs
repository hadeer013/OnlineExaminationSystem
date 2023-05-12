using exam.BLL.Interfaces;
using exam.DAL.Data;
using exam.DAL.Entities.QuestionsModule.MCQModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exam.BLL.Repositories
{
    public class MCQwithOneCorrectAnswerRepository : GenericRepository<MCQwithOneCorrectAns>, IMCQwithOneCorrectAnswerRepository
    {
        public MCQwithOneCorrectAnswerRepository(ExaminationContext context) : base(context)
        {
        }

        public async Task<bool> ValidateAnswer(List<string> options, string answer)
        {
            if (!options.Contains(answer))
            {
                return false;
            }
            return true;
        }

    }
}
