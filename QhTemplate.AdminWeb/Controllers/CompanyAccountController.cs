using Microsoft.AspNetCore.Mvc;

namespace QhTemplate.AdminWeb.Controllers
{
    public class CompanyAccountController : Controller
    {
        // GET
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }
    }
}