using Microsoft.AspNetCore.Mvc;

namespace QhTemplate.AdminWeb.Controllers
{
    public class RoleController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return  View();
        }
    }
}