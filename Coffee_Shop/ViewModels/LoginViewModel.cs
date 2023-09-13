using System.ComponentModel.DataAnnotations;

namespace Coffee_Shop.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Enter the name")]
        [MaxLength(20, ErrorMessage = "The name must be less than 20 symbols")]
        [MinLength(3, ErrorMessage = "The name must be more than 3 symbols")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Enter the password")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
