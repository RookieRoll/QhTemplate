using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QhTemplate.FontWeb.Models.ViewCompentModels;
using QhTemplate.MysqlEntityFrameWorkCore.Models;
using Remotion.Linq.Parsing.ExpressionVisitors.Transformation.PredefinedTransformations;

namespace QhTemplate.FontWeb.ViewComponents
{
    public class BriefingContentViewComponent : ViewComponent
    {
        private readonly EmsDBContext _dbContext;
        private readonly IHttpContextAccessor _accessor;
        public BriefingContentViewComponent(EmsDBContext dbContext, IHttpContextAccessor accessor)
        {
            _dbContext = dbContext;
            _accessor = accessor;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            string[] weekdays = { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };
            var list = (from briefing in _dbContext.BriefingContent
                        join school in _dbContext.SchoolArea on briefing.SchoolId equals school.Id
                        where briefing.StartTime > DateTime.Now.Date
                        orderby briefing.StartTime
                        select new BrifingViewModel
                        {
                            Id = briefing.Id,
                            Company = briefing.CompanyName,
                            School = school.Name,
                            Time = briefing.StartTime.ToString("MM-dd HH:mm"),
                            Tag = weekdays[(int)briefing.StartTime.DayOfWeek]
                        }).Take(40);
            var count = list.Count() / 2;
            BriefingContentViewModel model = new BriefingContentViewModel();
            model.list1 = list.Take(count).ToList();
            model.list2 = list.Skip(count).ToList();

            return View("BrifingContent", model);
        }
    }

    public class BriefingContentViewModel
    {
        public List<BrifingViewModel> list1 { get; set; }
        public List<BrifingViewModel> list2 { get; set; }
    }
}