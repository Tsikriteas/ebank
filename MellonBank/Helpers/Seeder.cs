using MellonBank.Data;
using MellonBank.Models;
using Microsoft.AspNetCore.Identity;

namespace MellonBank.Helpers
{
	public class Seeder
	{
		public async Task Seed(AppDbContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
		{
			if (!context.Users.Any())
			{
				var roles = new[] { "Employee", "Customer" };

				// Create roles if they don't exist
				foreach (var role in roles)
				{
					if (!await roleManager.RoleExistsAsync(role))
						await roleManager.CreateAsync(new IdentityRole(role));
				}

				// Create a test employee
				await CreateUserAndAssignRole(userManager, "Test", "Testopoulos", "test@test.com", "Tester", "6985236541", "00000000000", "Athens", "Test123!", "Employee");

				// Create a test customer
				await CreateUserAndAssignRole(userManager, "Fanis", "Tsikriteas", "fanis@gmail.com", "tsi", "6999999999", "1234567891", "Athens, 12345", "Fanis123!", "Customer");

				// Create an account for the customer
				var customer = await userManager.FindByNameAsync("tsi");
				if (customer != null)
				{
					var account = new AccountData
					{
						AccountBalance = 10000,
						AccountNumber = "GR12345678912345",
						AccountType = "Checking",
						Branch = "Kentro",
						CustomerAfm = "1234567891",
						User = customer
					};
					context.Accounts.Add(account);
				}

				await context.SaveChangesAsync();
			}
			if (!context.Rates.Any())
			{
				MellonRates rates = new MellonRates
				{
					AUD = 1,
					CHF = 1,
					GBP = 1,
					USD = 1,
				};
				context.Rates.Add(rates);
				context.SaveChanges();
			}
		}

		private async Task CreateUserAndAssignRole(UserManager<User> userManager, string firstName, string lastName, string email, string userName, string phone, string afm, string address, string password, string roleName)
		{
			var user = new User
			{
				FisrtName = firstName,
				LastName = lastName,
				Email = email,
				UserName = userName,
				Afm = afm,
				Address = address,
				PhoneNumber = phone,
				SecurityStamp = Guid.NewGuid().ToString()
			};

			var result = await userManager.CreateAsync(user, password);

			if (result.Succeeded)
			{
				await userManager.AddToRoleAsync(user, roleName);
			}
		}
	}
}
