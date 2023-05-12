using exam.DAL.Entities.QuestionsModule;
using exam.DAL.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace exam.PL.Dtos.ExamDtos
{
    public class ExamQuestionsDistributionDto
    {
        public int ChapterId { get; set; }
        public string? ChapterName { get; set; }
        public QustionDifficulty qustionDifficulty { get; set; }
        public QuestionTypes QuestionType { get; set; }
        public int NumOfQuestion { get; set; }
    }
}
