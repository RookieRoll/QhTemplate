using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QhTemplate.AdminWeb.Navigation;

namespace QhTemplate.AdminWeb.ViewComponents.MenuViewComponents
{
    public class MenuViewComponent:ViewComponent
    {
        private readonly MenuProvider _mPrivider;
        public MenuViewComponent(MenuProvider mPrivider)
        {
            _mPrivider = mPrivider;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = HttpContext.User.Claims.SingleOrDefault(x => x.Type.Equals(ClaimTypes.Sid))?.Value;
            var id = int.Parse(user);
            var result =await Task.FromResult(_mPrivider.GetMenuByUserId(id));
            return View("_Menu", result);
        }
    }
}