using System;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using QhTemplate.MysqlEntityFrameWorkCore.Models;

namespace QhTemplate.AdminWeb.Utils
{
    public class AccountServiceUtil
    {
        public static async Task SaveSignInUserIndetifier(HttpContext httpContext,User user)
        {
            var userIdentity = new ClaimsPrincipal(
                new ClaimsIdentity(
                    new[]
                    {
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(ClaimTypes.Sid, user.Id.ToString()),
                        new Claim(ClaimTypes.Role,user.UserType.ToString())
                    },
                    CookieAuthenticationDefaults.AuthenticationScheme
                ));

            await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                userIdentity,
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.Now.Add(TimeSpan.FromDays(7)) // 有效时间
                });
        }

        public  static bool IsEmailLogin(string value)
        {
            const string regexStr = @"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
            return Regex.IsMatch(value, regexStr);
        }
    }
}