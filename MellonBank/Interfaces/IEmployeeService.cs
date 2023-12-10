using MellonBank.Models;
using MellonBank.ViewModels;

namespace MellonBank.Interfaces
{
	public interface IEmployeeService
	{
		IEnumerable<User>? GetCustomers();
		User GetSearchCustomer(string afm);
		CustomerDetailsViewModel GetDetails(string afm);
		Task<bool> CreateCustomer(CreateCustomerViewModel model);
		Task<bool> CreateBankAccount(string afm, BankAccountViewModel model);
		string GenerateRandomGRNumber();
		EditViewModel Edit(string afm);
		Task<bool> Edit(string afm, EditViewModel EditModel);
		Task<bool> DeleteConfirmed(string afm);
		Task<bool> DeleteAccount(string accountNumber);
	}
}
