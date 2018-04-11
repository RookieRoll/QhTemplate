using Microsoft.AspNetCore.Mvc;
using QhTemplate.AdminWeb.ViewModels.CompanyAccounts;
using QhTemplate.ApplicationService.Companys;
using QhTemplate.ApplicationService.Users;

namespace QhTemplate.AdminWeb.Controllers
{
    public class CompanyAccountController : Controller
    {
        private readonly ICompanyService _companyService;
        private readonly IUserAppService _userAppService;

        public CompanyAccountController(ICompanyService companyService, IUserAppService userAppService)
        {
            _companyService = companyService;
            _userAppService = userAppService;
        }

        // GET
        public IActionResult SignIn()
        {
            
            return View("_SignIn");
        }

        public IActionResult SignIn(SignViewModel model)
        {
            var company = _companyService.Find(model.CompanyId);
            var user = _userAppService.GetUsersByCompany(company.Id);
            
            return Json("");
        }

        [HttpGet]
        public IActionResult SignUpCompany()
        {
            return View("_SignCompany");
        }
        
        [HttpGet]
        public IActionResult SignUpUser()
        {
            return View("_SignUser");
        }
    }
}