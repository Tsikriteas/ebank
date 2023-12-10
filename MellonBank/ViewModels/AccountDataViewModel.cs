using MellonBank.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MellonBank.ViewModels
{
	public class AccountDataViewModel
	{
		[Display(Name = "Account number")]
		public string AccountNumber { get; set; } = string.Empty;
		[Display(Name = "Afm")]
		public string CustomerAfm { get; set; } = string.Empty;
		[Display(Name = "Branch")]
		public string Branch { get; set; } = string.Empty;
		[Display(Name = "AccountType")]
		public string AccountType { get; set; } = string.Empty;
		[Display(Name = "Balnace")]
		public decimal AccountBalance { get; set; }
		[Display(Name = "Other currency Balance")]
		public decimal otherCurrencyBalance { get; set; }
	}

}
