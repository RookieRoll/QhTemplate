using Microsoft.AspNetCore.Mvc;
using QhTemplate.MysqlEntityFrameWorkCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QhTemplate.FontWeb.ViewComponents
{
    public class AreaViewComponent:ViewComponent
    {
       private readonly EmsDBContext _dbContext;
        public AreaViewComponent(EmsDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var list = new List<Area>();
            _dbContext.Area.ToList().ForEach(m =>
            {
                if (m.Path.Split(',').Length == 3)
                {
                    list.Add(m);
                }
            });

            var name = _dbContext.Area.FirstOrDefault(m=>m.Id==id).Name;
            AreaListViewModel model = new AreaListViewModel
            {
                CurrentName = name,
                list = list.Select(m => new AreaViewModel
                {
                    Id = m.Id,
                    Name = m.Name
                }).ToList()
            };
            return View("AreaModel", model);
        }
        
    }

    public class AreaListViewModel
    {
        public string CurrentName { get; set; }
        public List<AreaViewModel> list { get; set; }
    }

    public class AreaViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
