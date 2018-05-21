using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QhTemplate.AdminWeb.ViewModels.Account;
using QhTemplate.MysqlEntityFrameWorkCore.Models;

namespace QhTemplate.AdminWeb.ViewComponents.HeaderViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var name = await Task.FromResult(HttpContext.User.Identities.SingleOrDefault(x => x.NameClaimType.Equals(ClaimTypes.Name))?.Name);
            var id = await Task.FromResult(HttpContext.User.Claims.SingleOrDefault(x => x.Type.Equals(ClaimTypes.Sid))?.Value);
            var role = HttpContext.User.Claims.SingleOrDefault(x => x.Type.Equals(ClaimTypes.Role))?.Value;
            var ids = Convert.ToInt32(id);
            var type = int.Parse(role);
            var model = new HeaderShowViewModel
            {
                Id = ids,
                Name = name,
                Login= GetLogoutUrl(role),
                UserType = type
            };
            return View("_Header", model);
        }

        private string GetLogoutUrl(string type)
        {
            var origin = int.Parse(type);
            var types = (UserType)origin;
            switch (types)
            {
                case UserType.Admin: return "Account/Signin";
                case UserType.Teacher: return "schoolaccount/Signin";
                default: return "CompanyAccount/Signin";
            }
        }
    }
}