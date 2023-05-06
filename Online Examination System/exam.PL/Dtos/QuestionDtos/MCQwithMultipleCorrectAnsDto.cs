namespace exam.PL.Dtos.QuestionDtos
{
    public class MCQwithMultipleCorrectAnsDto:QustionDto
    {
        public List<string> Options { get; set; } = new List<string>();
        public List<string> Answers { get; set; } = new List<string>();
    }
}
