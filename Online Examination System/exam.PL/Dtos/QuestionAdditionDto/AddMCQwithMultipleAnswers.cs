using exam.PL.Dtos.QuestionDtos;

namespace exam.PL.Dtos.QuestionAdditionDto
{
    public class AddMCQwithMultipleAnswers:AddQuestionDto
    {
        
        public List<string> Answers { get; set; }
        public List<string> Options { get; set; }

    }
}
