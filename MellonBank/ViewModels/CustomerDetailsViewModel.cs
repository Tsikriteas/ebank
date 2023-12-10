using System.ComponentModel.DataAnnotations;

namespace MellonBank.ViewModels
{
	public class CustomerDetailsViewModel
	{

		[Display(Name = "Firstname")]
		public string FirstName { get; set; } = string.Empty;

		[Display(Name = "Lαstname")]
		public string LastName { get; set; } = string.Empty;


		[Display(Name = "Email")]
		public string Email { get; set; } = string.Empty;


		[Display(Name = "Afm")]
		public string customerAfm { get; set; } = string.Empty;
		[Display(Name = "Address")]
		public string Address { get; set; } = string.Empty;
		[Display(Name = "Phone number")]
		public string PhoneNumber { get; set; } = string.Empty;

		[Display(Name = "Accounts")]
		public List<AccountDataViewModel> Accounts { get; set; }

	}
}
