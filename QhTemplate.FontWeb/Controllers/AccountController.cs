using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QhTemplate.ApplicationCore.Exceptions;
using QhTemplate.ApplicationService.Schools;
using QhTemplate.ApplicationService.Users;
using QhTemplate.FontWeb.Models.Accounts;
using QhTemplate.FontWeb.Service;
using QhTemplate.MysqlEntityFrameWorkCore.Models;

namespace QhTemplate.FontWeb.Controllers
{
    public class AccountController : Controller
    {
        private readonly EmsDBContext _dbContext;
        private readonly IUserAppService _userApp;
        private readonly ISchoolService _schoolService;
        public AccountController(EmsDBContext dbContext, IUserAppService schoolService, ISchoolService schoolService1)
        {
            _dbContext = dbContext;
            _userApp = schoolService;
            _schoolService = schoolService1;
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return PartialView("SignIN");
        }

        public async Task<IActionResult> SignIn(AccountsViewModel model)
        {
            var schoolArea = _dbContext.SchoolArea.FirstOrDefault(m => m.Name.Equals(model.SchoolName)) ??
                             throw new UserFriendlyException("该学校不在本系统中");
            var users = _userApp.GetUsersBySchool(schoolArea.Id);
            //判断是否是邮箱登陆

            bool Func(User m) => m.UserName.Equals(model.UserName) && m.Password.Equals(model.Password) && m.UserType == (int) UserType.Student;
            var user = users.FirstOrDefault(Func);
            if (user == null)
            {
                return Json("账号或者密码错误");
            }
            await AccountServiceUtil.SaveSignInUserIndetifier(HttpContext, user);
            return Json("ok");
        }

        public async Task<IActionResult> LoginOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("index", "Home");
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View("SignUp");
        }

        [HttpPost]
        public IActionResult GetSchool(string name)
        {
            name = name ?? "";
            var schools = _dbContext.SchoolArea.Where(m => m.Name.Contains(name)).Select(m => new
            {
                id = m.Id,
                name = m.Name
            });
            return Json(schools);
        }

        public async Task<IActionResult> SignUp(AccountSignUp obj)
        {
            var schoolArea = _dbContext.SchoolArea.FirstOrDefault(m => m.Name.Equals(obj.SchoolName)) ??
                        throw new UserFriendlyException("该学校不在本系统中");

            var user = _userApp.Register(obj.UserName, obj.Name, obj.Email, obj.Password, UserType.Student);
            _schoolService.AddTeachers(schoolArea.Id, user);
            await AccountServiceUtil.SaveSignInUserIndetifier(HttpContext, new User { Id=user,UserName=obj.Name,UserType=(int)UserType.Student});
            return Json("ok");
        }
    }

}
