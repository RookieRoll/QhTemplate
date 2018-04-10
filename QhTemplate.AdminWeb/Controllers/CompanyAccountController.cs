using Microsoft.AspNetCore.Mvc;
using QhTemplate.AdminWeb.ViewModels.CompanyAccounts;

namespace QhTemplate.AdminWeb.Controllers
{
    public class CompanyAccountController : Controller
    {
        // GET
        public IActionResult SignIn()
        {
            return View();
        }

        public IActionResult SignIn(SignViewModel model)
        {
            return Json("");
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }
    }
}