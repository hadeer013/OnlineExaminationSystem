namespace exam.PL.Dtos.QuestionDtos
{
    public class MCQwithOneCorrectAnsDto:QustionDto
    {
        public List<string> Options { get; set; } = new List<string>();
        public string? Answer { get; set; }
    }
}
