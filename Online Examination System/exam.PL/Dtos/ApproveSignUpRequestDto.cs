using System.ComponentModel.DataAnnotations;

namespace exam.PL.Dtos
{
    public class ApproveSignUpRequestDto
    {
        [Required]
        public int RequestId { get; set; }
        [Required]
        public bool IsApproved { get; set; }
    }
}
