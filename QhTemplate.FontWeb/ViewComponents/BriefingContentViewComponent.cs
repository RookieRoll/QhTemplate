using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QhTemplate.FontWeb.Models.ViewCompentModels;
using QhTemplate.MysqlEntityFrameWorkCore.Models;
using Remotion.Linq.Parsing.ExpressionVisitors.Transformation.PredefinedTransformations;

namespace QhTemplate.FontWeb.ViewComponents
{
    public class BriefingContentViewComponent : ViewComponent
    {
        private readonly EmsDBContext _dbContext;

        public BriefingContentViewComponent(EmsDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var list = from briefing in _dbContext.BriefingContent
                join school in _dbContext.SchoolArea on briefing.SchoolId equals school.Id
                where briefing.StartTime > DateTime.Now.Date
                select new BrifingViewModel
                {
                    Id = briefing.Id,
                    Company = briefing.CompanyName,
                    School = school.Name,
                    Time = briefing.StartTime.ToString("MM-dd HH:mm")
                };
            return View("BrifingContent",list);
        }
    }
}