using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MellonBank.ViewModels
{
	public class ChangePasswordViewModel : IdentityUser
	{
		[DataType(DataType.Password)]
		[Display(Name = "Old password")]
		[Required(ErrorMessage = "The Old Password field is required.")]
		public string OldPassword { get; set; } = string.Empty;

		[DataType(DataType.Password)]
		[Display(Name = "New password")]
		[Required(ErrorMessage = "The New Password field is required.")]
		public string NewPassword { get; set; } = string.Empty;

		[Display(Name = "Confirm password")]
		[Required(ErrorMessage = "The Confirm Password field is required.")]
		[Compare("NewPassword", ErrorMessage = "The password and confirmation password do not match.")]
		public string ConfirmPassword { get; set; } = string.Empty;

	}
}

