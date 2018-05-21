using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QhTemplate.FontWeb.Filer;
using QhTemplate.MysqlEntityFrameWorkCore.Models;

namespace QhTemplate.FontWeb.Controllers
{
    [MyAuthentications]
    public class FileOptionController : Controller
    {
        private readonly EmsDBContext _context;

        public FileOptionController(EmsDBContext context)
        {
            _context = context;
        }

        [HttpPost]
        [MyAuthentications]
        public async Task<IActionResult> Delivery(int companyId, int recruidId)
        {
            var userName = await Task.FromResult(HttpContext.User.Identities.SingleOrDefault(x => x.NameClaimType.Equals(ClaimTypes.Name))?.Name);
            var userId = await Task.FromResult(HttpContext.User.Claims.SingleOrDefault(x => x.Type.Equals(ClaimTypes.Sid))?.Value);
            var ids = int.Parse(userId);
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
                          where user.UserType == (int)UserType.Student && user.Id == ids
                          select sch.Name).FirstOrDefault();
            var fileName = $"{temp.RecruitName}-{school}-{userName}";

            var origin = _context.FileRelation.FirstOrDefault(m =>
                m.CompanyId == companyId && m.RecruitId == recruidId && m.UserId == ids);
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
                DisplayName = fileName,
                UserId = ids
            });
            _context.SaveChanges();
            return Json("ok");
        }
    }
}