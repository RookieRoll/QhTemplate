using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using QhTemplate.FontWeb.Models.Recruitments;
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

        public IActionResult Index(int id, int ids, string search = "", int page = 1)
        {
            search = search ?? "";
            RecruitmentViewmodel viewModel = new RecruitmentViewmodel
            {
                MenuType = 2,
                Page = page,
                TypeId = id
            };
            var areaids = new List<int>();
            var majors = new List<int>();
            var result = _db.Recruitment.ToList(); ;
            if (id != 0)
            {
                majors = _db.MajorRecruitMent.Where(m => m.MajorId == id).Select(m => m.RecruitMentId).ToList();
                result = _db.Recruitment.Where(m => majors.Any(n => n == m.Id)).ToList();
            }

            if (ids != 0)
            {
                areaids = _db.AreaRecruit.Where(m => m.AreaId == ids).Select(m => m.RecruitMentId).ToList();
                result = _db.Recruitment.Where(m => areaids.Any(n => n == m.Id)).ToList();
            }

            var companys = (from company in _db.Company
                            join recruit in result on company.Id equals recruit.CompanyId
                            where company.Name.Contains(search) || recruit.Title.Contains(search)
                            group new { recruit, company } by recruit.CompanyId into temp
                            select new RecruitmentModel
                            {
                                CompanyId = (int)temp.Key,
                                CompanyName = temp.FirstOrDefault().company.Name,
                                JobName = string.Join(",", temp.Select(m => m.recruit.Title)),
                                Email = temp.FirstOrDefault().company.Email
                            }).ToList();


            viewModel.Result = companys;
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult GetArea()
        {
            var areas = new List<AreaViewModel>();
            var origin = _db.Area.ToList();
            origin.ForEach(m =>
            {
                if (m.Path.Split(',').Length == 3)
                {
                    areas.Add(new AreaViewModel
                    {
                        Id = m.Id,
                        Name = m.Name
                    });
                }
            });
            return Json(areas);
        }

        public IActionResult Detail(int id)
        {
            DetailViewModel detail = _db.Company.Where(m => m.Id == id).Select(m => new DetailViewModel
            {
                CompanyId = m.Id,
                CompanyName = m.Name,
                Description = m.Description,
                Address=m.Address,
                Email=m.Email
            }).First();

            var recruits = _db.Recruitment
                .Where(m => m.CompanyId == detail.CompanyId && m.EndTime >= DateTime.Now.Date)
                .Select(m => new RecruitModel
            {
                    Id=m.Id,
                    Content=m.Content,
                    Title=m.Title
            }).ToList();
            detail.Recruits = recruits;
            return View(detail);

        }
    }
}