using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MellonBank.ViewModels
{
	public class EditViewModel : IdentityUser
	{
		[Display(Name = "Firstname")]
		public string FirstName { get; set; } = string.Empty;

		[Display(Name = "Lαstname")]
		public string LastName { get; set; } = string.Empty;
		[Required]
		[EmailAddress]
		[Display(Name = "Email")]
		public string Email { get; set; } = string.Empty;
		[Required]
		[Display(Name = "Address")]
		public string Address { get; set; } = string.Empty;
		[Required]
		[Phone]
		[Display(Name = "Phone number")]
		public string PhoneNumber { get; set; } = string.Empty;
		[Required]
		[RegularExpression("^[0-9]{10}$", ErrorMessage = "Afm must be a 10-digit number.")]
		[Display(Name = "Afm")]
		public string Afm { get; set; } = string.Empty;
	}
}
