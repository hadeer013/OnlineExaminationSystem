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
    public class MCQwithMultipleCorrectAnsRepository : GenericRepository<MCQwithMultipleCorrectAns>, IMCQwithMultipleCorrectAnsRepository
    {
        public MCQwithMultipleCorrectAnsRepository(ExaminationContext context) : base(context)
        {
        }
        public async Task<bool> ValidateAnswers(List<string> options, List<string> answers)
        {
            foreach (string answer in answers)
            {
                if (!options.Contains(answer))
                {
                    return false;
                }
            }
            if (answers.Distinct().Count() != answers.Count)
            {
                return false;
            }

            return true;
        }
        public async Task<int> CheckForCorrectness(List<string> CorrectAnswers, List<string> SubmitAnswers)
        {
            int numCorrect = 0;
            for (int i = 0; i < SubmitAnswers.Count; i++)
            {
                if (CorrectAnswers.Contains(SubmitAnswers[i]))
                {
                    numCorrect++;
                }
            }
            return numCorrect;
        }

    }
}
