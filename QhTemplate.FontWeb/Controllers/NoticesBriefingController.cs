using System;
using System.Linq;
using System.Security.Claims;
using IronPython.Modules;
using Microsoft.AspNetCore.Mvc;
using QhTemplate.ApplicationCore;
using QhTemplate.ApplicationCore.Exceptions;
using QhTemplate.FontWeb.Filer;
using QhTemplate.FontWeb.Models.NoticeBrefing;
using QhTemplate.MysqlEntityFrameWorkCore.Models;

namespace QhTemplate.FontWeb.Controllers
{
    [MyAuthentications]
    public class NoticesBriefingController : Controller
    {
        private readonly EmsDBContext _context;

        public NoticesBriefingController(EmsDBContext context)
        {
            _context = context;
        }

        // GET
        public IActionResult Index(string time="",string search = "")
        {
            var uid = GetUserId();
            var noticeList = (from notices in _context.NoticeBriefing
                              join briefs in _context.BriefingContent on notices.BriefingId equals briefs.Id
                              join schools in _context.SchoolArea on briefs.SchoolId equals schools.Id
                              where notices.UserId == uid && briefs.StartTime.Date >= DateTime.Now.Date
                              select new NoticeBriefingViewModel
                              {
                                  Id=notices.Id,
                                  BriefId=briefs.Id,
                                  Company=briefs.CompanyName,
                                  Held=schools.Name+briefs.Held,
                                  IsExpired=briefs.StartTime.Date>=DateTime.Now,
                                  Time=briefs.StartTime.ToString("MM-dd HH:mm"),
                                  PublishTime=briefs.PublishTime.ToString("MM-dd HH:mm")
                              }).ToList();
            return View(noticeList);
        }

        [HttpPost]
        public IActionResult SetNotices(int id)
        {
            var uid = GetUserId();
            var origin = _context.NoticeBriefing.FirstOrDefault(m => m.UserId == uid && m.BriefingId == id);
            if (origin != null) return Json("设置成功");
            _context.NoticeBriefing.Add(new NoticeBriefing
            {
                BriefingId = id,
                UserId = uid
            });
            _context.SaveChanges();
            return Json("设置成功");
        }
        [HttpPost]
        public IActionResult CancleNotices(int id)
        {
            var origin = _context.NoticeBriefing.FirstOrDefault(m => m.Id == id) ?? throw new UserFriendlyException("该提醒不存在");
            _context.NoticeBriefing.Remove(origin);
            _context.SaveChanges();
            return Json("取消成功");
        }

        private int GetUserId()
        {
            var userId = HttpContext.User.Claims.SingleOrDefault(x => x.Type.Equals(ClaimTypes.Sid))?.Value;
            var id = int.Parse(userId);
            return id;
        }
    }
}