using Microsoft.AspNetCore.Mvc;
using QhTemplate.FontWeb.Models.MenuBar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QhTemplate.MysqlEntityFrameWorkCore.Models;

namespace QhTemplate.FontWeb.ViewComponents
{
    public class ClassifiesMenuBarViewComponent : ViewComponent
    {
        private readonly EmsDBContext _context;

        public ClassifiesMenuBarViewComponent(EmsDBContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int type,int areaId)
        {
            var area = _context.Area.FirstOrDefault(m=>m.Id==areaId);
            var schools = _context.SchoolArea.Where(m => m.Path.StartsWith(area.Path)).Select(m=>new MenuBar()
            {
                Id = m.Id,
                Name = m.Name
            }).ToList();

            MenuBarViewModel bar = new MenuBarViewModel
            {
                AreaId = areaId,
                MenuBar = schools
            };
            return View("MenuBar",bar);
        }
    }
}
