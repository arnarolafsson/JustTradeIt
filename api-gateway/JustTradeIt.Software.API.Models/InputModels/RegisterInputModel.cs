using System.ComponentModel.DataAnnotations;

namespace JustTradeIt.Software.API.Models.InputModels
{
    public class RegisterInputModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "Name is to short!")]
        public string FullName { get; set; }
        [Required]
        [MinLength(8, ErrorMessage = "Password is to short!")]
        public string Password { get; set; }
        [Required]
        [MinLength(8)]
        [Compare("Password", ErrorMessage = "Passwords do not match!")]
        public string PasswordConfirmation { get; set; }
    }
}