using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QhTemplate.ApplicationService.Utils.EmailUtils;
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
            var userId = await Task.FromResult(HttpContext.User.Claims.SingleOrDefault(x => x.Type.Equals(ClaimTypes.Sid))?.Value);
            var ids = int.Parse(userId);
            var temp = (from company in _context.Company
                        join recruid in _context.Recruitment on company.Id equals recruid.CompanyId
                        where company.Id == companyId && recruid.Id == recruidId
                        select new
                        {
                            company.Email,
                            RecruitName = recruid.Title,
                            CompanyName = company.Name
                        }).FirstOrDefault();

            var school = (from sch in _context.SchoolArea
                          join relation in _context.SchoolUser on sch.Id equals relation.SchoolId
                          join user in _context.User on relation.UserId equals user.Id
                          where user.UserType == (int)UserType.Student && user.Id == ids
                          select sch.Name).FirstOrDefault();

            var username = _context.User.FirstOrDefault(m => m.Id == ids)?.Name;
            var fileName = $"{temp.RecruitName}-{school}-{username}";

            var origin = _context.FileRelation.FirstOrDefault(m =>
                m.CompanyId == companyId && m.RecruitId == recruidId && m.UserId == ids);
            if (origin == null)
            {
                _context.FileRelation.Add(new FileRelation
                {
                    CompanyId = companyId,
                    RealName = userId,
                    RecruitId = recruidId,
                    CreateTime = DateTime.Now,
                    DisplayName = fileName,
                    UserId = ids
                });
                _context.SaveChanges();

            }
            else
            {
                if (!origin.IsDeleted)
                {
                    return Json("ok");
                }
            }



            EmailHelper emailHelper = new EmailHelper();
            var filename = companyId + "@" + userId + "@" + fileName;
            var url = "http://localhost:54791/FileDownload/DownLoad?file=" + filename;
            EmailRecevierConfig emailRecevierConfig = new EmailRecevierConfig
            {
                EmailAdd = temp.Email,
                Addressee = temp.CompanyName,
                Content = string.Format("您有收到一份简历【{0}】<a href='{1}'>下载</a>" +
                "或者进入<a href='http://localhost:59932/CompanyAccount/Signin'>就业网</a>进行查看", fileName, url),
                Subject = "有人向您投递了一份简历，请注意查收-就业网"

            };
            await emailHelper.SendEmailAsync(emailRecevierConfig);
            return Json("ok");
        }
    }
}