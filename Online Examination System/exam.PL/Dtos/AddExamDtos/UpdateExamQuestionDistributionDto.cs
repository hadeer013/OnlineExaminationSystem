using exam.DAL.Entities.QuestionsModule;

namespace exam.PL.Dtos.AddExamDtos
{
    public class UpdateExamQuestionDistributionDto
    {
        public int Id { get; set; }
        public int ChapterId { get; set; }
        public QustionDifficulty qustionDifficulty { get; set; }
        public QuestionTypes QuestionType { get; set; }
        public int NumOfQuestion { get; set; }
    }
}
