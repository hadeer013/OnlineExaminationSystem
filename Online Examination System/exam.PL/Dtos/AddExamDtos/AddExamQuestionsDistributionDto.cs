using exam.DAL.Entities.QuestionsModule;
using exam.DAL.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace exam.PL.Dtos.AddExamDtos
{
    public class AddExamQuestionsDistributionDto
    {
        public int ChapterId { get; set; }
        public QustionDifficulty qustionDifficulty { get; set; }
        public QuestionTypes QuestionType { get; set; }
        public int NumOfQuestion { get; set; }
    }
}
