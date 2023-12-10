using MellonBank.Models;
using MellonBank.ViewModels;

namespace MellonBank.Interfaces
{
	public interface ICustomerService
	{
		User GetCustomer(string afm);
		IEnumerable<AccountData> GetAccounts(string afm);
		Task<bool> ChangePassword(string afm, ChangePasswordViewModel newPassword);
		IEnumerable<AccountDataViewModel> GetCustomerAccounts(string afm);
		Task<AccountDataViewModel> GetAccountDetails(string accountNumber);
		string Transaction(string afm, string fromAccount, string toAccount, decimal amount);
	}
}
