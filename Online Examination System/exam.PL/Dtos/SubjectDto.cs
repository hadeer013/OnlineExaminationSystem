using exam.DAL.Entities.ProfessorEntities;
using exam.DAL.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace exam.PL.Dtos
{
    public class SubjectDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? ProfessorId { get; set; }
        public string? ProfessorName { get; set; }
        public int LevelId { get; set; }
        public string? LevelName { get; set; }
        public int DepartmentId { get; set; }
        public string? DepartmentName { get;set; }
    }
}
