using Microsoft.AspNetCore.Mvc;

namespace QhTemplate.AdminWeb.Controllers
{
    public class CareerTalkController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}