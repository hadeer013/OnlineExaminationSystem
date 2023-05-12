using exam.DAL.Entities.QuestionsModule;

namespace exam.PL.Dtos.ExamDtos
{
    public class QuestionTypePointDto
    {
        public int Id { get; set; }
        public QuestionTypes QuestionType { get; set; }
        public float Point { get; set; }
    }
}
