using System;
using System.Linq;
using System.Threading.Tasks;
using IronPython.Modules;
using Microsoft.AspNetCore.Mvc;
using QhTemplate.AdminWeb.Models;
using QhTemplate.AdminWeb.Navigation;
using QhTemplate.AdminWeb.Utils;
using QhTemplate.AdminWeb.ViewModels.CompanyAccounts;
using QhTemplate.ApplicationCore.Exceptions;
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

        #region 登陆
        // GET
        [HttpGet]
        public IActionResult SignIn()
        {

            return View("_SignIn");
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignViewModel model)
        {
            var company = _companyService.FirstOrDefault(m=>m.Name.Equals(model.CompanyId))??throw new UserFriendlyException("该公司不存在");
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
                return Json("账号或者密码错误");
            }

            await AccountServiceUtil.SaveSignInUserIndetifier(HttpContext, user);
            _menuProvider.RemoveMenu(user.Id);
            _menuProvider.LoadMenu(user.Id);
            return Json("ok");
        }


        #endregion

        #region 注册
        [HttpGet]
        public IActionResult SignUpComnpany()
        {
            return View("_SignCompany");
        }

        [HttpPost]
        public IActionResult SignUpCompany(CompanySignUpViewModel model)
        {
            var id = _companyService.Creat(model.Name, model.Address, model.LegalPerson, model.Tellphone, model.Email);
            return Json(new JsonModel { Status = true, Message = model.Name });
        }

        public IActionResult SignUpUser(string name)
        {
            name = name ?? "";
            return View("_SignUser",name);
        }
        [HttpPost]
        public IActionResult SignUpUser(SignUpViewModel model)
        {
            var company = _companyService.FirstOrDefault(m=>m.Name.Equals(model.CompanyId))??throw new UserFriendlyException("公司不存在");
            var user = _userAppService.Register(model.UserName, model.Name, model.Email, model.Password,
                UserType.Employee);

            _companyService.SetCompanyUser(company.Id, user);
            return Json("ok");
        }
        #endregion


        public IActionResult GetCompany(string name)
        {
            name = name ?? "";
            var company = _companyService.Finds(m => m.Name.Contains(name)).Select(m => new
            {
                id = m.Id,
                name = m.Name
            });
            return Json(company);
        }
    }
}