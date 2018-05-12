using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using QhTemplate.MysqlEntityFrameWorkCore.Models;

namespace QhTemplate.FontWeb.Controllers
{
    public class FileOptionController : Controller
    {
        private readonly EmsDBContext _context;

        public FileOptionController(EmsDBContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Delivery(int companyId, int recruidId)
        {
            var userId = 1;
            var userName = "";
            var temp = (from company in _context.Company
                join recruid in _context.Recruitment on company.Id equals recruid.CompanyId
                where company.Id == companyId && recruid.Id == recruidId
                select new
                {
                    Email = company.Email,
                    RecruitName = recruid.Title
                }).FirstOrDefault();

            var school = (from sch in _context.SchoolArea
                join relation in _context.SchoolUser on sch.Id equals relation.SchoolId
                join user in _context.User on relation.UserId equals user.Id
                where user.UserType == (int) UserType.Student && user.Id == userId
                select sch.Name).FirstOrDefault();
            var fileName = $"{temp.RecruitName}-{school}-{userName}";

            var origin = _context.FileRelation.FirstOrDefault(m =>
                m.CompanyId == companyId && m.RecruitId == recruidId && m.UserId == userId);
            if (origin != null)
            {
                _context.FileRelation.Remove(origin);
                _context.SaveChanges();
            }

            _context.FileRelation.Add(new FileRelation()
            {
                CompanyId = companyId,
                RealName = userId.ToString(),
                RecruitId = recruidId,
                CreateTime = DateTime.Now,
                DisplayName = fileName
            });
            _context.SaveChanges();
            return Json("投递成功");
        }
    }
}