using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace QhTemplate.AdminWeb.ViewComponents.HeaderViewComponents
{
    public class UserViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var name = await Task.FromResult(HttpContext.User.Identities.SingleOrDefault(x => x.NameClaimType.Equals(ClaimTypes.Name))?.Name);

            return View("_User", name);
        }
    }
}
