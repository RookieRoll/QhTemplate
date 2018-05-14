using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QhTemplate.FontWeb.Models.Recruitments;
using QhTemplate.MysqlEntityFrameWorkCore.Models;

namespace QhTemplate.FontWeb.ViewComponents
{
    public class RecruitViewComponent : ViewComponent
    {
        private readonly EmsDBContext _dbContext;

        public RecruitViewComponent(EmsDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var ids = _dbContext.MajorRecruitMent.Select(m => m.RecruitMentId)
                .Concat(_dbContext.AreaRecruit.Select(m => m.RecruitMentId));

            var result = (from company in _dbContext.Company
                join recruit in _dbContext.Recruitment on company.Id equals recruit.CompanyId
                where ids.Any(m => m == recruit.Id)
                group new {recruit, company} by recruit.CompanyId
                into temp
                select new RecruitmentModel
                {
                    CompanyId = (int) temp.Key,
                    CompanyName = temp.FirstOrDefault().company.Name,
                    JobName =temp.FirstOrDefault().recruit.CreateTime.ToString("MM-dd"),
                    Email = temp.FirstOrDefault().company.Email
                }).Take(40).ToList();
            var count = result.Count() / 2;
            var tempModal = new RecruitList();
            tempModal.list1 = result.Take(count).ToList();
            tempModal.list2 = result.Skip(count).ToList();
            return View("Recruit",tempModal);
        }
    }

    public class RecruitList
    {
        public List<RecruitmentModel> list1 { get; set; }
        public List<RecruitmentModel> list2 { get; set; }
    }
}