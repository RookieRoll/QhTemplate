using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using QhTemplate.AdminWeb.ViewModels.Account;
using QhTemplate.ApplicationService.Users;
using QhTemplate.ApplicationService.Utils;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using QhTemplate.AdminWeb.Navigation;
using QhTemplate.AdminWeb.Utils;
using QhTemplate.MysqlEntityFrameWorkCore.Models;

namespace QhTemplate.AdminWeb.Controllers
{
    public class AccountController : Controller
    {
        private readonly IMemoryCache _cache;
        private readonly IUserAppService _userApp;
        private readonly MenuProvider _menuProvider;

        public AccountController(IMemoryCache cache, IUserAppService userApp, MenuProvider menuProvider)
        {
            _cache = cache;
            _userApp = userApp;
            _menuProvider = menuProvider;
        }

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(AccountViewModel login)
        {
            if (!ModelState.IsValid) return Json("输入信息错误");
            if (!CheckValidateCode(login.ValidateCode))
                return Json("验证码错误");

            Func<User, bool> func = null;
            //判断是否是邮箱登陆
            if (AccountServiceUtil.IsEmailLogin(login.UserName))
                func = m => m.EmailAddress.Equals(login.UserName) && m.Password.Equals(login.Password);
            else
                func = m => m.UserName.Equals(login.UserName) && m.Password.Equals(login.Password);

            var user = _userApp.FirstOrDefault(func);
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

        //        private async Task SaveSignInUserIndetifier(User user)
        //        {
        //            var userIdentity = new ClaimsPrincipal(
        //                new ClaimsIdentity(
        //                    new[]
        //                    {
        //                        new Claim(ClaimTypes.Name, user.UserName),
        //                        new Claim(ClaimTypes.Sid, user.Id.ToString())
        //                    },
        //                    CookieAuthenticationDefaults.AuthenticationScheme
        //                ));
        //
        //            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
        //                userIdentity,
        //                new AuthenticationProperties
        //                {
        //                    IsPersistent = true,
        //                    ExpiresUtc = DateTimeOffset.Now.Add(TimeSpan.FromDays(7)) // 有效时间
        //                });
        //        }
        //
        //        private bool IsEmailLogin(string value)
        //        {
        //            const string regexStr = @"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
        //            return Regex.IsMatch(value, regexStr);
        //        }

        private bool CheckValidateCode(string code)
        {
            return _cache.Get(HttpContext.Connection.LocalIpAddress) is string originCode &&
                   originCode.Equals(code, StringComparison.OrdinalIgnoreCase);
        }

        public async Task<IActionResult> SignOut(string url)
        {
            var param = url.Trim().Split('/');
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(param[1], param[0]);
            //return RedirectToAction("SignIn", "Account");
        }

        public IActionResult ValidateCode()
        {
            var ms = ValidateCodeServiceUtil.CreateValidateCode(out string code);
            var key = HttpContext.Connection.LocalIpAddress;
            _cache.Set(key, code);
            return File(ms.ToArray(), @"image/png");
        }
    }
}