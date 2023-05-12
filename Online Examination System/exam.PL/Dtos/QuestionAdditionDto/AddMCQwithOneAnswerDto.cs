namespace exam.PL.Dtos.QuestionAdditionDto
{
    public class AddMCQwithOneAnswerDto:AddQuestionDto
    {
        public List<string> Options { get; set; }
        public string CorrectAnswer { get; set; }
    }
}
