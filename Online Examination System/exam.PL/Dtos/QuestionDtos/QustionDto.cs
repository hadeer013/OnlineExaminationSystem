using exam.DAL.Entities.QuestionsModule;
using exam.DAL.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace exam.PL.Dtos.QuestionDtos
{
    public class QustionDto
    {
        public int Id { get; set; }
        public string? questionContent { get; set; }
        public QustionDifficulty QuestionDifficulty { get; set; }
        public  QuestionTypes QuestionType { get; set; }
        public int ChapterId { get; set; }
        public string? ChapterName { get; set; }
    }
}
