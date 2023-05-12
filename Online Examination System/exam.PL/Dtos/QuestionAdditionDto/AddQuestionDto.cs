using exam.DAL.Entities.QuestionsModule;
using System.ComponentModel.DataAnnotations;

namespace exam.PL.Dtos.QuestionAdditionDto
{
    public class AddQuestionDto
    {
        public IFormFile? QuestionFormFile { get; set; }
        [Required]
        public string? questionContent { get; set; }
        [Required]
        public QustionDifficulty QuestionDifficulty { get; set; }
        [Required]
        public virtual QuestionTypes QuestionType { get; }
        [Required]
        public int ChapterId { get; set; }
    }
}
