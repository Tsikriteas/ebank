using AutoMapper;
using MellonBank.Data;
using MellonBank.Interfaces;
using MellonBank.Models;
using MellonBank.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MellonBank.Services
{
	public class CustomerService : ICustomerService
	{
		private readonly AppDbContext _context;
		private readonly IMapper _mapper;
		private readonly UserManager<User> _userManager;

		public CustomerService(AppDbContext context, IMapper mapper, UserManager<User> user)
		{
			_context = context;
			_mapper = mapper;
			_userManager = user;
		}
		public User GetCustomer(string afm)
		{
			var user = _context.Users.SingleOrDefault(x => x.Afm == afm);
			return user;
		}
		public IEnumerable<AccountData> GetAccounts(string afm)
		{
			var accounts = _context.Accounts.Where(x => x.CustomerAfm == afm).ToList();
			return accounts;
		}

		//Post
		public async Task<bool> ChangePassword(string afm, ChangePasswordViewModel newPassword)
		{
			var customer = GetCustomer(afm);
			if (customer == null)
			{
				return false;
			}
			if (newPassword.NewPassword != newPassword.ConfirmPassword)
			{
				return false;
			}
			var changePasswordResult = await _userManager.ChangePasswordAsync(customer, newPassword.OldPassword, newPassword.NewPassword);
			if (!changePasswordResult.Succeeded)
			{
				return false;
			}
			return true;
		}

		public IEnumerable<AccountDataViewModel> GetCustomerAccounts(string afm)
		{
			var customer = GetCustomer(afm);
			if (customer == null)
			{
				return null;
			}
			var accounts = GetAccounts(afm);
			List<AccountDataViewModel> customerAccounts = _mapper.Map<List<AccountDataViewModel>>(accounts);

			return customerAccounts;
		}

		public async Task<AccountDataViewModel> GetAccountDetails(string AccountNumber)
		{
			var account = _context.Accounts.SingleOrDefault(x => x.AccountNumber == AccountNumber);
			if (account == null)
			{
				return null;
			}

			using (HttpClient client = new HttpClient())
			{
				try
				{
					HttpResponseMessage response = await client.GetAsync("https://api.freecurrencyapi.com/v1/latest?apikey=fca_live_xTdpNAgsWp7cfLlyIShnzHC3n392XRq2TIJrXP2O");
					if (response.IsSuccessStatusCode)
					{
						string apiResponse = await response.Content.ReadAsStringAsync();
						ExchangeRates exchangeRatesEur = JsonConvert.DeserializeObject<ExchangeRates>(apiResponse);
						decimal usdRate = (decimal)(exchangeRatesEur.Data.EUR * (1 + 0.05));

						AccountDataViewModel accountViewmodel = _mapper.Map<AccountDataViewModel>(account);
						accountViewmodel.otherCurrencyBalance = Math.Round((accountViewmodel.AccountBalance * usdRate), 2);

						return accountViewmodel;
					};
				}
				catch (Exception ex) { return null; }
				return null;
			}
		}
		public class ExchangeRates
		{
			public ExchangeRateData Data { get; set; }
		}

		public class ExchangeRateData
		{
			public double EUR { get; set; }
		}
		public string Transaction(string afm, string fromAccount, string toAccount, decimal amount)
		{
			var account = _context.Accounts.Where(x => x.AccountNumber == fromAccount).FirstOrDefault();
			var tomyAccount = _context.Accounts.Where(x => x.AccountNumber == toAccount).FirstOrDefault();

			if (account == null)
			{
				return "Invalid account or customer.";
			}
			if (account.AccountBalance < amount)
			{
				return "Insufficient funds.";
			}
			if (account.CustomerAfm == afm)
			{

				if (tomyAccount != null)
				{
					account.AccountBalance -= amount;
					tomyAccount.AccountBalance += amount;
					_context.SaveChanges();
					return "Success!";
				}
				//money not to my account
				else
				{
					account.AccountBalance -= amount;

					_context.SaveChanges();
					return "Success!";
				}
			}
			return "Unauthorized transaction.";
		}
	}
}

