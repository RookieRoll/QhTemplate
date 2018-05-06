using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite.Internal.UrlActions;
using Microsoft.Azure.KeyVault;
using QhTemplate.AdminWeb.Navigation;
using QhTemplate.AdminWeb.Utils;
using QhTemplate.AdminWeb.ViewModels.SchoolAccount;
using QhTemplate.ApplicationCore.Exceptions;
using QhTemplate.ApplicationService.Schools;
using QhTemplate.ApplicationService.Users;
using QhTemplate.MysqlEntityFrameWorkCore.Models;

namespace QhTemplate.AdminWeb.Controllers
{
    public class SchoolAccountController : Controller
    {
        private readonly ISchoolService _schoolService;
        private readonly IUserAppService _userAppService;
        private readonly MenuProvider _menuProvider;

        public SchoolAccountController(ISchoolService schoolService, IUserAppService appService,
            MenuProvider menuProvider)
        {
            _schoolService = schoolService;
            _userAppService = appService;
            _menuProvider = menuProvider;
        }

        public IActionResult Signin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetSchool(string name)
        {
            name =name ?? "";
            var schools = _schoolService.Finds(m => m.Name.Contains(name)).Select(m => new
            {
                id = m.Id,
                name = m.Name
            });
            return Json(schools);
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SchoolSignIN model)
        {
            var schoolArea = _schoolService.Finds(m => m.Name.Equals(model.SchoolName)).FirstOrDefault() ??
                          throw new UserFriendlyException("该学校不在本系统中");

            var users = _userAppService.GetUsersByCompany(schoolArea.Id);
            Func<User, bool> func;
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

            await AccountServiceUtil.SaveSignInUserIndetifier(HttpContext, user);
            _menuProvider.RemoveMenu(user.Id);
            _menuProvider.LoadMenu(user.Id);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult SignUp()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult SignUp(SignUpViewModel obj)
        {
            var schoolArea = _schoolService.Finds(m => m.Name.Equals(obj.SchoolName)).FirstOrDefault() ??
                             throw new UserFriendlyException("该学校不在本系统中");

           var user = _userAppService.Register(obj.UserName, obj.Name, obj.Email, obj.Password, UserType.Teacher);
            _schoolService.AddTeachers(schoolArea.Id,user);
            return RedirectToAction("SignIn");
        }
    }
}