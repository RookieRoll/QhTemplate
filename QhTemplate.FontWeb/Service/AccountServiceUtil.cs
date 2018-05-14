using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using QhTemplate.MysqlEntityFrameWorkCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace QhTemplate.FontWeb.Service
{
    public class AccountServiceUtil
    {
        public static async Task SaveSignInUserIndetifier(HttpContext httpContext, User user)
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
    }
}
