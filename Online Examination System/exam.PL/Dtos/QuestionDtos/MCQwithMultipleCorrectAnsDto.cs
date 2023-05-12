namespace exam.PL.Dtos.QuestionDtos
{
    public class MCQwithMultipleCorrectAnsDto: MCQwithOneCorrectAnsDtoForStudent
    {
        public IEnumerable<AnswerDto> Answers { get; set; } =new HashSet<AnswerDto>();
    }
}
