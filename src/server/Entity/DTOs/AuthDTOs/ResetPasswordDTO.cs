using System.ComponentModel.DataAnnotations;

namespace Entity.DTOs.AuthDTOs
{
    public class ResetPasswordDTO
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "Password have to be at least 8 characters required!")]
        [MaxLength(15, ErrorMessage = "Password must be no more than 15 characters!")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm password is required")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
    }
}
