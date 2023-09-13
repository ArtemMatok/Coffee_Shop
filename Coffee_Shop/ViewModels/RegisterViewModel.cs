using System.ComponentModel.DataAnnotations;

namespace Coffee_Shop.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Enter name")]
        [MaxLength(20, ErrorMessage = "The name must be less than 20 symbols")]
        [MinLength(3, ErrorMessage = "The name must be more than 3 symbols")]
        public string Name { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Enter the password")]
        [MinLength(6, ErrorMessage = "The password must be more than 6 symbols")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Passwords is not equal")]
        public string PasswordConfirm { get; set; }
    }
}
