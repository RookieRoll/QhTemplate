﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QhTemplate.ApplicationCore;
using QhTemplate.ApplicationService.BriefingContents;
using QhTemplate.FontWeb.Models.BriefingContents;
using QhTemplate.MysqlEntityFrameWorkCore.Models;

namespace QhTemplate.FontWeb.Controllers
{
    public class BriefingContentController : Controller
    {
        private readonly IBriefingContentService _contentSerivce;
        private readonly EmsDBContext _db;
        private const int Size = BaseConst.Size;

        public BriefingContentController(IBriefingContentService contentSerivce, EmsDBContext db)
        {
            _contentSerivce = contentSerivce;
            _db = db;
        }

        public IActionResult Index(int id, int ids, string time, string search = "")
        {
            search = search ?? "";
            BriefingContentViewModel result = new BriefingContentViewModel()
            {
                AreaId = id,
                Page = 1,
                MenuType = 1,
                SchoolId = ids
            };

            List<int> school = new List<int>();
            if (id == 0)
            {
                school = (from area in _db.SchoolArea
                          select area.Id).ToList();
            }
            else
            {
                school = (from area in _db.SchoolArea
                          where area.Path.Contains(string.Format(",{0},", id))
                          select area.Id).ToList();
            }



            var tempContent = from content in _db.BriefingContent
                              where school.Any(m => m.Equals(content.SchoolId))
                              select content;
            Func<BriefingContent, bool> func;
            if (ids == 0)
            {
                func = m => m.StartTime > DateTime.Now.Date;
            }
            else
            {
                func = m => m.StartTime > DateTime.Now.Date && m.SchoolId == ids;
            }

            Func<BriefingContent, bool> filter;
            if (string.IsNullOrWhiteSpace(time))
            {
                filter = m => m.CompanyName.Contains(search);
            }
            else
            {
                filter = m => m.CompanyName.Contains(search) && m.StartTime.Date.Equals(DateTime.Parse(time).Date);
            }

            result.Result = tempContent.Where(func).Where(filter).OrderBy(m => m.StartTime).Take(Size).Select(m =>
                new BriefingContentList
                {
                    Id = m.Id,
                    Company = m.CompanyName,
                    Held = m.Held,
                    StartTime = m.StartTime.ToString("yyyy-MM-dd HH:mm"),
                    PublishTime = m.PublishTime.ToString("yyyy-MM-dd HH:mm")
                }).ToList();
            return View(result);
        }

        [HttpPost]
        public IActionResult GetMore(int id, int ids, int page = 1)
        {
            page -= 1;
            var school = from area in _db.SchoolArea
                         where area.Path.Contains(string.Format(",{0},", id))
                         select area.Id;
            var tempContent = from content in _db.BriefingContent
                              where school.Any(m => m.Equals(content.SchoolId))
                              select content;
            var result = tempContent.OrderBy(m => m.StartTime).Skip(page * Size).Take(Size).Select(m =>
                    new BriefingContentList
                    {
                        Id = m.Id,
                        Company = m.CompanyName,
                        Held = m.Held,
                        StartTime = m.StartTime.ToString("yyyy-MM-dd HH:mm"),
                        PublishTime = m.PublishTime.ToString("yyyy-MM-dd HH:mm")
                    }).ToList();

            return PartialView("_Content", result);
        }

        public IActionResult Detail(int id)
        {
            var detail = (from briefing in _db.BriefingContent
                          join school in _db.SchoolArea on briefing.SchoolId equals school.Id
                          where briefing.Id == id
                          select new DetailViewModel
                          {
                              Id = briefing.Id,
                              CompanyName = briefing.CompanyName,
                              Content = briefing.Content,
                              Held = briefing.Held,
                              PublishTime = briefing.PublishTime.ToString("yyyy-MM-dd HH:mm"),
                              StartTime = briefing.StartTime.ToString("yyyy-MM-dd HH:mm"),
                              SchoolName = school.Name
                          }).FirstOrDefault();
            return View(detail);
        }
    }
}