using Microsoft.AspNetCore.Mvc;
using QhTemplate.FontWeb.Models.MenuBar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QhTemplate.MysqlEntityFrameWorkCore.Models;
using System.Security.Claims;

namespace QhTemplate.FontWeb.ViewComponents
{
    public class ClassifiesMenuBarViewComponent : ViewComponent
    {
        private readonly EmsDBContext _context;

        public ClassifiesMenuBarViewComponent(EmsDBContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int type, int areaId)
        {
            var username = await Task.FromResult(HttpContext.User.Identities.SingleOrDefault(x => x.NameClaimType.Equals(ClaimTypes.Name))?.Name);
            List<MenuBar> result;
            if (type == 1)
            {
                if (areaId == 0)
                {
                    result = _context.SchoolArea.Select(m => new MenuBar()
                    {
                        Id = m.Id,
                        Name = m.Name
                    }).ToList();
                }
                else
                {
                    var area = _context.Area.FirstOrDefault(m => m.Id == areaId);
                    result = _context.SchoolArea.Where(m => m.Path.StartsWith(area.Path)).Select(m => new MenuBar()
                    {
                        Id = m.Id,
                        Name = m.Name
                    }).ToList();
                }


            }
            else
            {
                result = _context.Major.Select(m => new MenuBar
                {
                    Id = m.Id,
                    Name = m.Name
                }).ToList();
            }


            MenuBarViewModel bar = new MenuBarViewModel
            {
                AreaId = areaId,
                MenuBar = result,
                MenuType = type,
                UserName = username
            };
            return View("MenuBar", bar);
        }
    }
}
