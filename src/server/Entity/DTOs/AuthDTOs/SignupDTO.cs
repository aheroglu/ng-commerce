using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DTOs.AuthDTOs
{
    public class SignupDTO
    {
        [Required(ErrorMessage = "User name is required!")]
        [MinLength(8, ErrorMessage = "User name have to be at least 8 characters required!")]
        [MaxLength(15, ErrorMessage = "User name must be no more than 15 characters")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Phone number is required")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "Password have to be at least 8 characters required!")]
        [MaxLength(15, ErrorMessage = "Password must be no more than 15 characters!")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm password is required")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
    }
}
