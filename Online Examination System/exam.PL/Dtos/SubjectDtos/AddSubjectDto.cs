using System.ComponentModel.DataAnnotations;

namespace exam.PL.Dtos.SubjectDtos
{
    public class AddSubjectDto
    {
        [Required]
        public string Name { get; set; }
        public string? ProfessorId { get; set; }
        [Required]
        public int LevelId { get; set; }
        [Required]
        public int DepartmentId { get; set; }
    }
}
