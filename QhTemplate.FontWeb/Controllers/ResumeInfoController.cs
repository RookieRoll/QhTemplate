using Microsoft.AspNetCore.Mvc;
using QhTemplate.MysqlEntityFrameWorkCore.Models;
using System.Linq;
using System.Security.Claims;
using QhTemplate.ApplicationCore;
using QhTemplate.ApplicationService.Recruitments;
using QhTemplate.FontWeb.Filer;
using QhTemplate.FontWeb.Models.ResumeInfo;


namespace QhTemplate.FontWeb.Controllers
{
    [MyAuthentications]
    public class ResumeInfoController : Controller
    {
        private readonly EmsDBContext _context;
        private readonly int Size = BaseConst.Size;
        public ResumeInfoController(EmsDBContext context)
        {
            _context = context;
        }

        // GET
        public IActionResult Index()
        {
            var userId = HttpContext.User.Claims.SingleOrDefault(x => x.Type.Equals(ClaimTypes.Sid))?.Value;
            var ids = int.Parse(userId);
            var result = (from resumes in _context.FileRelation
                    join companys in _context.Company on resumes.CompanyId equals companys.Id
                    join recruits in _context.Recruitment on resumes.RecruitId equals recruits.Id
                    where resumes.UserId == ids
                    orderby resumes.CreateTime descending
                    select new ResumeInfoViewModel
                    {
                        CompanyId = companys.Id,
                        CompanyName = companys.Name,
                        JobName = recruits.Title,
                        ResumeTime = resumes.CreateTime.ToString("yyyy-MM-dd"),
                        Id = resumes.Id
                    }
                ).Take(Size).ToList();
            return View(result);
        }

        public IActionResult GetMoreDate(int page=1)
        {
            page -= 1;
            var userId = HttpContext.User.Claims.SingleOrDefault(x => x.Type.Equals(ClaimTypes.Sid))?.Value;
            var ids = int.Parse(userId);
            var result = (from resumes in _context.FileRelation
                    join companys in _context.Company on resumes.CompanyId equals companys.Id
                    join recruits in _context.Recruitment on resumes.RecruitId equals recruits.Id
                    where resumes.UserId == ids
                    orderby resumes.CreateTime descending
                    select new ResumeInfoViewModel
                    {
                        CompanyId = companys.Id,
                        CompanyName = companys.Name,
                        JobName = recruits.Title,
                        ResumeTime = resumes.CreateTime.ToString("yyyy-MM-dd"),
                        Id = resumes.Id
                    }
                ).Skip(page*Size).Take(Size).ToList();

            return PartialView("_Record",result);

        }
    }
}