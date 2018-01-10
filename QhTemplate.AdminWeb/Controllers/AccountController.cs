using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using QhTemplate.ApplicationService.Utils;

namespace QhTemplate.AdminWeb.Controllers
{
    public class AccountController : Controller
    {
        private readonly IMemoryCache _cache;

        public AccountController(IMemoryCache cache)
        {
            _cache = cache;
        }

        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SignIn(string username)
        {
            return Json("");
        }


        public IActionResult SignUp()
        {
            return Json("");
        }

        public IActionResult SignOut()
        {
            return Json(1);
        }
        public IActionResult ValidateCode()
        {
            var ms = ValidateCodeServiceUtil.CreateValidateCode(out string code);
            var key = HttpContext.Connection.Id;
            _cache.Set(key, code);
            return File(ms.ToArray(), @"image/png");
        }
    }
}