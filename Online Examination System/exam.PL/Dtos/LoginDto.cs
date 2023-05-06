using System.ComponentModel.DataAnnotations;

namespace exam.PL.Dtos
{
    public class LoginDto
    {
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
