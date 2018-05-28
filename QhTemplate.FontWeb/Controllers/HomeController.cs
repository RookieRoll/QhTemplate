using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using QhTemplate.FontWeb.Models;
using QhTemplate.FontWeb.Service;
using QhTemplate.MysqlEntityFrameWorkCore.Models;

namespace QhTemplate.FontWeb.Controllers
{
    public class HomeController : Controller
    {

        private readonly IHostingEnvironment _environmentl;

        public HomeController(IHostingEnvironment environmentl)
        {
            _environmentl = environmentl;
        }

        public IActionResult Index()
        {
            EmsDBContext context=new EmsDBContext();
            TimedJobServices job=new TimedJobServices(context,_environmentl);
            job.Run();
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public async Task<IActionResult> GetMyResume()
        {
            var id = await Task.FromResult(HttpContext.User.Claims.SingleOrDefault(x => x.Type.Equals(ClaimTypes.Sid))?.Value);
            id += ".pdf";
            return PartialView("MyResume", id);
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}