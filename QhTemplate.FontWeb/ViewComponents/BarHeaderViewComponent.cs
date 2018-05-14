using Microsoft.AspNetCore.Mvc;
using QhTemplate.MysqlEntityFrameWorkCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace QhTemplate.FontWeb.ViewComponents
{
    public class BarHeaderViewComponent:ViewComponent
    {
        private readonly EmsDBContext _dbContext;

        public BarHeaderViewComponent(EmsDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var name = await Task.FromResult(HttpContext.User.Identities.SingleOrDefault(x => x.NameClaimType.Equals(ClaimTypes.Name))?.Name);
            var id = await Task.FromResult(HttpContext.User.Claims.SingleOrDefault(x => x.Type.Equals(ClaimTypes.Sid))?.Value);
            UserViewModel mode = new UserViewModel
            {
                Name = name,
                IsLogin = string.IsNullOrWhiteSpace(name),
                UserId=id+".pdf"
            };
            return View("BarHeader", mode);
        }
    }

    public class UserViewModel
    {
        public string Name { get; set; }

        public bool IsLogin { get; set; }

        public string  UserId { get; set; } 
    }
}
