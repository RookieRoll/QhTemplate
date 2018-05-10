using System.Linq;
using Microsoft.AspNetCore.Mvc;
using QhTemplate.MysqlEntityFrameWorkCore.Models;

namespace QhTemplate.FontWeb.Controllers
{
    public class RecruitmentController : Controller
    {
        private readonly EmsDBContext _db;

        public RecruitmentController(EmsDBContext db)
        {
            _db = db;
        }

        public IActionResult Index(int classId,int areaId,string search,int page=1)
        {
            var ids = _db.AreaRecruit.Where(m => m.AreaId == areaId).Select(m=>m.RecruitMentId)
                .Union(_db.MajorRecruitMent.Where(m => m.MajorId == classId).Select(m=>m.RecruitMentId));

            var result = _db.Recruitment.Where(m => ids.Any(n => n == m.Id)).OrderByDescending(m => m.CreateTime);
            return View();
        }
    }
}