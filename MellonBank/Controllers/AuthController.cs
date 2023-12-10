using MellonBank.Models;
using MellonBank.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MellonBank.Controllers
{
	public class AuthController : Controller
	{
		private readonly SignInManager<User> _signInManager;
		private readonly ILogger<AuthController> _logger;
		private readonly UserManager<User> _userManager;

		public AuthController(SignInManager<User> signInManager, UserManager<User> userManager, ILogger<AuthController> logger)
		{
			_signInManager = signInManager;
			_userManager = userManager;
			_logger = logger;
		}

		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginModel model)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByEmailAsync(model.Email);
				if (user != null)
				{
					var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, model.RememberMe, lockoutOnFailure: false);
					if (result.Succeeded)
					{
						_logger.LogInformation("User logged in.");
						TempData["SuccessMessage"] = "Login successful!";

						var role = await _userManager.GetRolesAsync(user);

						//Redirect depends role
						if (role.Contains("Customer"))
						{
							return RedirectToAction("Index", "Customer", new { afm = user.Afm });
						}
						else if (role.Contains("Employee"))
						{
							return RedirectToAction("Index", "Employee");
						}
					}
					if (result.IsLockedOut)
					{
						_logger.LogWarning("User account locked out.");
						return RedirectToPage("./Lockout");
					}
					else
					{
						ModelState.AddModelError(string.Empty, "Invalid login attempt.");
						return View();
					}
				}
				else
				{
					ModelState.AddModelError(string.Empty, "Invalid login attempt.");
					return View();
				}
			}
			return View();
		}
	}
}

