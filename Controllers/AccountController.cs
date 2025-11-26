using Microsoft.AspNetCore.Mvc;
using SalesService.Models;
using SalesService.Services;
using SalesService.Helpers;

namespace SalesService.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IConfiguration _configuration;

        public AccountController(IAuthService authService, IConfiguration configuration)
        {
            _authService = authService;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Login()
        {
            // If already logged in, redirect to home
            if (SessionHelper.IsLoggedIn(HttpContext.Session))
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Build connection string key: NasanSales, NasanTraining, AsaanSales, AsaanTraining
                    string connectionKey = $"{model.Company}{model.Database}";
                    string connectionString = _configuration.GetConnectionString(connectionKey);

                    if (string.IsNullOrEmpty(connectionString))
                    {
                        ModelState.AddModelError("", "Invalid company or database selection");
                        return View(model);
                    }

                    // Validate user using Entity Framework
                    var result = await _authService.ValidateUserAsync(model.Username, model.Password, connectionString);

                    if (result.Success)
                    {
                        // Set session data
                        SessionHelper.SetEmployeeId(HttpContext.Session, result.EmployeeId);
                        SessionHelper.SetUsername(HttpContext.Session, result.Username);
                        SessionHelper.SetCompany(HttpContext.Session, model.Company);
                        SessionHelper.SetDatabase(HttpContext.Session, model.Database);
                        SessionHelper.SetConnectionString(HttpContext.Session, connectionString);

                        // Redirect to dashboard
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", result.Message);
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"An error occurred: {ex.Message}");
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Logout()
        {
            SessionHelper.Clear(HttpContext.Session);
            return RedirectToAction("Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout(string dummy)
        {
            SessionHelper.Clear(HttpContext.Session);
            return RedirectToAction("Login");
        }
    }
}