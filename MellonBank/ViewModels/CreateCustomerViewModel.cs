using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MellonBank.ViewModels
{
    public class CreateCustomerViewModel : IdentityUser
    {
        [Required(ErrorMessage = "Πρέπει να βάλεις όνομα!")]
        [Display(Name = "Name")]
        public string FisrtName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Πρέπει να βάλεις επώνυμο!")]
        [Display(Name = "Lastname")]
        public string LastName { get; set; } = string.Empty;
        [Required]
        [RegularExpression("^[0-9]{10}$", ErrorMessage = "Afm must be a 10-digit number.")]
        [Display(Name = "Afm")]
        public string Afm { get; set; } = string.Empty;
        [Required]
        [Display(Name = "Address")]
        public string Address { get; set; } = string.Empty;
        [Required]
        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;
        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; } = string.Empty;
        [Required]
        [DataType(DataType.Password)]
        [Compare("ConfirmPassword", ErrorMessage = "Passwords should match!")]
        [Display(Name = "Password")]
        public string Password { get; set; } = string.Empty;
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
