using MellonBank.Services;
using MellonBank.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MellonBank.Controllers
{
	[Authorize(Roles = "Employee")]
	public class EmployeeController : Controller
	{
		private readonly EmployeeService _employeeService;


		public EmployeeController(EmployeeService employeeService)
		{
			_employeeService = employeeService;
		}

		// GET: ApplicationUserController
		public ActionResult Index()
		{
			return View(_employeeService.GetCustomers());
		}

		public ActionResult IndexSearch(string afm)
		{
			var customer = _employeeService.GetSearchCustomer(afm);
			if (customer == null)
			{
				TempData["UserNotFound"] = "Ο χρήστης δεν υπάρχει";
				return RedirectToAction("Index");
			}

			return RedirectToAction("Details", new { afm = customer.Afm });
		}

		// GET: ApplicationUserController/Details/5
		public ActionResult Details(string afm)
		{
			return View(_employeeService.GetDetails(afm));
		}

		// GET: ApplicationUserController/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: ApplicationUserController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create(CreateCustomerViewModel model)
		{
			if (ModelState.IsValid)
			{
				var result = await _employeeService.CreateCustomer(model);

				if (result == true)
				{
					return RedirectToAction("Index");
				}
				else
				{
					ModelState.AddModelError(string.Empty, "Failed");
				}
			}
			return View(model);
		}

		// GET: ApplicationUserController/Create
		public ActionResult CreateBankAccount()
		{
			var viewmodel = new BankAccountViewModel();
			return View(viewmodel);
		}

		// POST: ApplicationUserController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> CreateBankAccount(string afm, BankAccountViewModel model)
		{
			if (ModelState.IsValid)
			{
				var result = await _employeeService.CreateBankAccount(afm, model);
				if (result == true)
				{
					return RedirectToAction("Index");
				}
				else
				{
					ModelState.AddModelError(string.Empty, "Failed");
				}
			}
			return View(model);
		}

		// GET: ApplicationUserController/Edit/5
		public ActionResult Edit(string afm)
		{
			var result = _employeeService.Edit(afm);
			if (result == null)
			{
				return NotFound();
			}
			ViewBag.UserName = $"{result.FirstName} {result.LastName}";

			return View(result);
		}

		// POST: ApplicationUserController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(string afm, EditViewModel EditModel)
		{
			if (!ModelState.IsValid)
			{
				return View(EditModel);
			}
			var result = _employeeService.Edit(afm, EditModel);
			if (result == null)
			{
				return NotFound();
			}
			else
			{
				ModelState.AddModelError("Afm", "Invalid Afm. Please enter the correct Afm.");
				return View(EditModel);
			}
		}

		// GET: ApplicationUserController/Delete/5
		[HttpGet]
		public ActionResult Delete(string afm)
		{
			if (afm == null)
			{
				return NotFound();
			}
			ViewBag.Email = $"{afm}";
			return View();
		}

		// POST: ApplicationUserController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> DeleteConfirmed(string afm)
		{
			var result = await _employeeService.DeleteConfirmed(afm);
			if (result == false)
			{
				return NotFound();
			}
			return RedirectToAction("Index");
		}

		[HttpPost]
		public async Task<ActionResult> DeleteAccount(string afm, string accountNumber)
		{
			var result = await _employeeService.DeleteAccount(accountNumber);
			if (result == false)
			{
				return NotFound();
			}
			return RedirectToAction("Details", new { afm = afm });
		}
	}
}
