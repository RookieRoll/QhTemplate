using Microsoft.AspNetCore.Mvc;

namespace QhTemplate.AdminWeb.Controllers
{
    public class SchoolAccountController : Controller
    {
 
        public IActionResult Signin()
        {
            return View();
        }

        public IActionResult SignIn(object obj)
        {
            return Json("");
        }

        public IActionResult SignUp()
        {
            return View();
        }

        public IActionResult SignUp(object obj)
        {
            return Json("");
        }
    }
}