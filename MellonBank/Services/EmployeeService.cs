using AutoMapper;
using MellonBank.Data;
using MellonBank.Interfaces;
using MellonBank.Models;
using MellonBank.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace MellonBank.Services
{
	public class EmployeeService : IEmployeeService
	{

		private readonly AppDbContext _context;
		private readonly UserManager<User> _userManager;
		private readonly IMapper _mapper;

		public EmployeeService(AppDbContext context, UserManager<User> userManager, IMapper mapper)
		{
			_context = context;
			_userManager = userManager;
			_mapper = mapper;
		}

		public IEnumerable<User>? GetCustomers()
		{
			var customers = _userManager.GetUsersInRoleAsync("Customer").Result.ToList();
			return customers;
		}
		public User GetSearchCustomer(string afm)
		{
			var customer = _userManager.Users.SingleOrDefault(u => u.Afm == afm);
			return customer;
		}
		//GET CustomerDetailsViewModel => (USER + DATA) VIEWMODEL
		public CustomerDetailsViewModel GetDetails(string afm)
		{
			var customer = GetSearchCustomer(afm);
			if (customer == null)
			{
				return null;
			}
			var Accounts = _context.Accounts.Where(x => x.CustomerAfm == customer.Afm).ToList();

			var viewmodel = new CustomerDetailsViewModel
			{
				FirstName = customer.FisrtName,
				LastName = customer.LastName,
				Email = customer.Email,
				customerAfm = customer.Afm,
				Address = customer.Address,
				PhoneNumber = customer.PhoneNumber,

				Accounts = Accounts.Select(Account => new AccountDataViewModel
				{
					AccountNumber = Account.AccountNumber,
					AccountBalance = Account.AccountBalance
				}).ToList()
			};
			return viewmodel;
		}

		public async Task<bool> CreateCustomer(CreateCustomerViewModel model)
		{

			User user = _mapper.Map<User>(model);

			var result = await _userManager.CreateAsync(user, model.Password);

			if (result.Succeeded)
			{
				await _userManager.AddToRoleAsync(user, "Customer");
				return true;
			}
			else
			{
				return false;
			}
		}

		public async Task<bool> CreateBankAccount(string afm, BankAccountViewModel model)
		{

			var randomGRNumber = GenerateRandomGRNumber();

			var customer = GetSearchCustomer(afm);
			if (customer == null)
			{
				return false;
			}
			AccountData account = _mapper.Map<AccountData>(model);
			account.AccountNumber = randomGRNumber.ToString();
			account.User = customer;
			_context.Accounts.Add(account);
			_context.SaveChanges();
			return true;
		}

		//function to generate Account Number
		public string GenerateRandomGRNumber()
		{
			const string prefix = "GR";
			const int numericLength = 14;

			var random = new Random();
			var numericPart = new string(Enumerable.Repeat("0123456789", numericLength)
				.Select(s => s[random.Next(s.Length)]).ToArray());
			var randomGRNumber = $"{prefix}{numericPart}";

			return randomGRNumber;
		}

		public EditViewModel Edit(string afm)
		{

			var customer = GetSearchCustomer(afm);
			if (customer == null)
			{
				return null;
			}
			EditViewModel edit = _mapper.Map<EditViewModel>(customer);
			return edit;
		}

		public async Task<bool> Edit(string afm, EditViewModel EditModel)
		{
			var customer = GetSearchCustomer(afm);
			if (customer == null)
			{
				return false;
			}
			if (EditModel.Afm == customer.Afm)
			{
				customer.Address = EditModel.Address;
				customer.PhoneNumber = EditModel.PhoneNumber;
				_context.SaveChanges();
				return true;
			}
			else
			{
				return false;
			}
		}
		public async Task<bool> DeleteConfirmed(string afm)
		{
			var customer = GetSearchCustomer(afm);
			if (customer == null)
			{
				return false;
			}
			_context.Users.Remove(customer);
			_context.SaveChanges();
			return true;
		}

		public async Task<bool> DeleteAccount(string accountNumber)
		{
			if (accountNumber == null)
			{
				return false;
			}

			var account = _context.Accounts.FirstOrDefault(x => x.AccountNumber == accountNumber);
			if (account != null)
			{
				_context.Accounts.Remove(account);
				await _context.SaveChangesAsync();
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}
