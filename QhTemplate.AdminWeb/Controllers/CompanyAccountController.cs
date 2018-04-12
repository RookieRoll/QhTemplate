using System;
using System.Linq;
using System.Threading.Tasks;
using IronPython.Modules;
using Microsoft.AspNetCore.Mvc;
using QhTemplate.AdminWeb.Navigation;
using QhTemplate.AdminWeb.Utils;
using QhTemplate.AdminWeb.ViewModels.CompanyAccounts;
using QhTemplate.ApplicationService.Companys;
using QhTemplate.ApplicationService.Users;
using QhTemplate.MysqlEntityFrameWorkCore.Models;

namespace QhTemplate.AdminWeb.Controllers
{
    public class CompanyAccountController : Controller
    {
        private readonly ICompanyService _companyService;
        private readonly IUserAppService _userAppService;
        private readonly MenuProvider _menuProvider;

        public CompanyAccountController(ICompanyService companyService, IUserAppService userAppService, MenuProvider menuProvider)
        {
            _companyService = companyService;
            _userAppService = userAppService;
            _menuProvider = menuProvider;
        }

        // GET
        public IActionResult SignIn()
        {
            
            return View("_SignIn");
        }

        public async Task<IActionResult> SignIn(SignViewModel model)
        {
            var company = _companyService.Find(model.CompanyId);
            var users = _userAppService.GetUsersByCompany(company.Id);
            Func<User, bool> func = null;
            //判断是否是邮箱登陆
            if (AccountServiceUtil.IsEmailLogin(model.UserName))
                func = m => m.EmailAddress.Equals(model.UserName) && m.Password.Equals(model.Password);
            else
                func = m => m.UserName.Equals(model.UserName) && m.Password.Equals(model.Password);

            var user = users.FirstOrDefault(func);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "账号或者密码错误");
                return RedirectToAction("SignIn");
            }

            await AccountServiceUtil.SaveSignInUserIndetifier(HttpContext,user);
            _menuProvider.RemoveMenu(user.Id);
            _menuProvider.LoadMenu(user.Id);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult SignUpCompany(CompanySignUpViewModel model)
        {
            var id = _companyService.Creat(model.Name, model.Address, model.LegalPerson, model.Tellphone);
            return Json(id);
        }
        
        [HttpGet]
        public IActionResult SignUpUser(SignUpViewModel model)
        {
            var company = _companyService.Find(model.CompanyId);
            var user = _userAppService.Register(model.UserName, model.Name, model.Email, model.Password,
                UserType.Employee);
            
            _companyService.SetCompanyUser(company.Id,user);
            return View("SignIn");
        }
    }
}