using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace MellonBank.ViewModels
{
	public class BankAccountViewModel
	{
		[Required]
		[Display(Name = "Customer afm")]
		public string CustomerAfm { get; set; } = string.Empty;
		[Required]
		[Display(Name = "Balance")]
		public decimal AccountBalance { get; set; }
		[Required]
		[Display(Name = "Branch")]
		public string Branch { get; set; } = string.Empty;
		[Required]
		[Display(Name = "Account type")]
		public string AccountType { get; set; } = string.Empty;
		public List<SelectListItem> AvailableAccountTypes { get; set; }
		public BankAccountViewModel()
		{
			AvailableAccountTypes = GetAvailableAccountTypes();
		}
		public List<SelectListItem> GetAvailableAccountTypes()
		{
			return new List<SelectListItem>
		{
			new SelectListItem { Value = "Checking", Text = "Checking accounts" },
			new SelectListItem { Value = "Savings", Text = "Savings accounts" },
			new SelectListItem { Value = "MoneyMarket", Text = "Money market accounts (MMAs)" },
			new SelectListItem { Value = "CD", Text = "Certificate of deposit accounts (CDs)" }
		};
		}
	}
}
