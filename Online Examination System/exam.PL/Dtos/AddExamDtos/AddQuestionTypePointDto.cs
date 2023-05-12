using exam.DAL.Entities.QuestionsModule;

namespace exam.PL.Dtos.AddExamDtos
{
    public class AddQuestionTypePointDto
    {
        public QuestionTypes QuestionType { get; set; }
        public float Point { get; set; }
    }
}
