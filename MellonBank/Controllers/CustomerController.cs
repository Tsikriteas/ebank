using MellonBank.Data;
using MellonBank.Models;
using MellonBank.Services;
using MellonBank.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace MellonBank.Controllers
{
	[Authorize(Roles = "Customer")]
	public class CustomerController : Controller
	{
		private readonly CustomerService _customerService;

		public CustomerController(CustomerService customerService)
		{
			_customerService = customerService;
		}
		// GET: ApplicationUserController
		public ActionResult Index(string afm)
		{
			var result = _customerService.GetCustomerAccounts(afm);
			if (result == null)
			{
				return NotFound();
			}
			return View(result);
		}
		[HttpGet]
		public ActionResult Details(string afm)
		{
			var result = _customerService.GetCustomer(afm);
			return View(result);
		}

		//GEt
		public ActionResult ChangePassword()
		{
			return View();
		}

		// POST: ApplicationUserController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> ChangePassword(string afm, ChangePasswordViewModel passwordModel)
		{
			if (!ModelState.IsValid)
			{
				return View(passwordModel);
			}
			var result = await _customerService.ChangePassword(afm, passwordModel);
			if (result)
			{
				TempData["SuccessMessage"] = "Login successful!";
				return RedirectToAction("Index", new { afm = afm });
			}
			else
			{
				ModelState.AddModelError("PasswordError", "Invalid password. Please enter the correct password.");
				return View(passwordModel);
			}
		}

		[HttpPost]
		public ActionResult Transaction(string afm, string fromAccount, string toAccount, decimal amount)
		{
			var result = _customerService.Transaction(afm, fromAccount, toAccount, amount);

			//error messages fix

			if (result == "Success!")
			{
				return RedirectToAction("Index", new { afm = afm });
			}
			else if (result == "Insufficient funds.")
			{
				return RedirectToAction("Error", new { message = "Insufficient funds." });
			}
			else
			{
				return RedirectToAction("Index");
			}
		}
		public ActionResult Error(string message)
		{
			var errorViewModel = new ErrorViewModel
			{
				ErrorMessage = message
			};
			return View("Error", errorViewModel);
		}


		public async Task<ActionResult> AccountDetails(string AccountNumber)
		{
			var result = await _customerService.GetAccountDetails(AccountNumber);
			if (result == null)
			{
				return NotFound();
			}
			else
			{
				return View(result);
			}
		}
	}
}