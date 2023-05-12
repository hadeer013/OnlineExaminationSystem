using exam.DAL.Entities.QuestionsModule;

namespace exam.PL.Dtos.AddExamDtos
{
    public class UpdateQuestionTypePointsDto
    {
        public int Id { get; set; }
        public QuestionTypes QuestionType { get; set; }
        public float Point { get; set; }
    }
}
