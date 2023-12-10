using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MellonBank.ViewModels
{
    public class LoginModel
    {

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; } = false;
    }
}
