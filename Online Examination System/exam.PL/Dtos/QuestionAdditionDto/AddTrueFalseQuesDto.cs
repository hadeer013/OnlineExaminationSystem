using System.ComponentModel.DataAnnotations;

namespace exam.PL.Dtos.QuestionAdditionDto
{
    public class AddTrueFalseQuesDto:AddQuestionDto
    {
        [Required]
        public bool Answer { get; set; }
    }
}
