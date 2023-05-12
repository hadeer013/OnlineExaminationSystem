using System.ComponentModel.DataAnnotations;

namespace exam.PL.Dtos
{
    public class StudentSignupDto
    {
        [Required]
        public string? userName { get; set; }
        [Required]
        public string? Code { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? PhoneNumber { get; set; }
        [Required]
        public string? Password {  get; set; }
        [Compare("Password", ErrorMessage = "Password and Confirmation Password must match")]
        [Required]
        public string? ConfirmPassword { get; set; }
        public string? Address { get; set; }
        [Required]
        public int LevelId { get; set; }
        [Required]
        public int DepartmentId { get; set; }
    }
}
