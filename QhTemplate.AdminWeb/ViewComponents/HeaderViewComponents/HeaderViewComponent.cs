using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QhTemplate.AdminWeb.ViewModels.Account;

namespace QhTemplate.AdminWeb.ViewComponents.HeaderViewComponents
{
    public class HeaderViewComponent:ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var name=await Task.FromResult(HttpContext.User.Identities.SingleOrDefault(x => x.NameClaimType.Equals(ClaimTypes.Name))?.Name);
            var id = await Task.FromResult(HttpContext.User.Claims.SingleOrDefault(x => x.Type.Equals(ClaimTypes.Sid))?.Value);
            
            var ids = Convert.ToInt32(id);
            var model = new HeaderShowViewModel
            {
                Id = ids,
                Name = name
            };
            return View("_Header",model);
        }
    }
}