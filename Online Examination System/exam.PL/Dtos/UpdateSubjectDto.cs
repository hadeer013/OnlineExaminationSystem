using exam.DAL.Entities.ProfessorEntities;
using exam.DAL.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace exam.PL.Dtos
{
    public class UpdateSubjectDto
    {
        public int Id { get; set; } 
        public string? Name { get; set; }
        public string? ProfessorId { get; set; }
        public int LevelId { get; set; }
        public int DepartmentId { get; set; }
    }
}
