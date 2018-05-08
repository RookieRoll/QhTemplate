using Microsoft.AspNetCore.Mvc;
using QhTemplate.FontWeb.Models.MenuBar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QhTemplate.FontWeb.ViewComponents
{
    public class ClassifiesMenuBarViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<MenuBarViewModel> list = new List<MenuBarViewModel>();
            list.Add(new MenuBarViewModel { Id = 1, Name = "12" });
            list.Add(new MenuBarViewModel { Id = 1, Name = "12" });
            list.Add(new MenuBarViewModel { Id = 1, Name = "12" });
            list.Add(new MenuBarViewModel { Id = 1, Name = "12" });
            list.Add(new MenuBarViewModel { Id = 1, Name = "12" });
            list.Add(new MenuBarViewModel { Id = 1, Name = "12" });
            return View("MenuBar",list);
        }
    }
}
