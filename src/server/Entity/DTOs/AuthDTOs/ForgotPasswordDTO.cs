using System.ComponentModel.DataAnnotations;

namespace Entity.DTOs.AuthDTOs
{
    public class ForgotPasswordDTO
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }
    }
}
