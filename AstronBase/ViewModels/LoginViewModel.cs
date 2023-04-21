using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace AstronBase.ViewModels
{
    public class LoginViewModel
    {
        [Required] [Display(Name = "Email")] public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Запомнить?")] public bool RememberMe { get; set; }

        public string? ReturnUrl { get; set; }
    }
}