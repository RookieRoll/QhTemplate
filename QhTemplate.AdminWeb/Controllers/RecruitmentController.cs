using System;
using System.Collections.Generic;
using System.Linq;
using DataTables.AspNet.AspNetCore;
using DataTables.AspNet.Core;
using Microsoft.AspNetCore.Mvc;
using QhTemplate.ApplicationService.Recruitments;
using System.Linq.Dynamic.Core;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using QhTemplate.AdminWeb.Filter;
using QhTemplate.AdminWeb.ViewModels.Organizations;
using QhTemplate.AdminWeb.ViewModels.Recruitments;
using QhTemplate.ApplicationService.Areas;
using QhTemplate.ApplicationService.Companys;
using QhTemplate.ApplicationService.Majors;
using QhTemplate.MysqlEntityFrameWorkCore.Models;
using QhTemplate.AdminWeb.Utils;

namespace QhTemplate.AdminWeb.Controllers
{
    [MyAuthentications]
    public class RecruitmentController : Controller
    {
        private readonly IRecruitmentServcie _recruitment;
        private readonly ICompanyService _company;
        private readonly IMajorAppService _majorApp;
        private readonly IAreaAppService _areaApp;
        public RecruitmentController(IRecruitmentServcie recruitment, ICompanyService company,
            IMajorAppService majorApp, IAreaAppService areaApp)
        {
            _recruitment = recruitment;
            _company = company;
            _majorApp = majorApp;
            _areaApp = areaApp;
        }


        // GET
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        public IActionResult Create(EditRecruitment obj)
        {
            var userId = HttpContext.User.Claims.SingleOrDefault(x => x.Type.Equals(ClaimTypes.Sid))?.Value;
            var id = int.Parse(userId);
            var companyId = _company.Finds().Include(m => m.CompanyUser).First(m => m.CompanyUser.Any(n => n.UserId == id));
            _recruitment.Create(obj.Title, PathUtil.PathReplace(obj.Content), DateTime.Parse(obj.EndTime), companyId.Id, obj.MajorIds, obj.AreaId);
            return Json("创建成功");
        }

        public IActionResult Update(int id)
        {
            var model = _recruitment.Find(id);
            return View("Update", EditRecruitment.Convert(model));
        }

        [HttpPost]
        public IActionResult Update(EditRecruitment obj)
        {
            var temp = new Recruitment
            {
                Id = obj.Id,
                Title = obj.Title,
                Content = PathUtil.PathReplace(obj.Content),
                EndTime = DateTime.Parse(obj.EndTime)
            };
            _recruitment.Update(temp, obj.MajorIds, obj.AreaId);
            return Json("修改成功");
        }

        public IActionResult Delete(int id)
        {
            var temp = _recruitment.Find(id);
            return PartialView("_Delete", RecruitmentViewModel.ConvertRecruitmentViewModel(temp));
        }

        public IActionResult DeleteComfirm(int id)
        {
            _recruitment.Remove(id);
            return Json("删除成功");
        }

        public IActionResult GetData(IDataTablesRequest request)
        {
            var data = _recruitment.Finds();

            var filteredData = string.IsNullOrWhiteSpace(request.Search.Value)
                ? data
                : data.Where(item => item.Title.Contains(request.Search.Value));

            var sortColumn = request.Columns.FirstOrDefault(c => c.Sort != null);
            if (sortColumn != null)
            {
                string ascending = sortColumn.Sort.Direction == SortDirection.Ascending ? "asc" : "desc";
                string orderStr = $"{sortColumn.Field} {ascending}";
                filteredData = filteredData.OrderBy(orderStr);
            }
            else
            {
                filteredData = filteredData.OrderByDescending(m => m.CreateTime).ThenBy(m => m.EndTime);
            }

            var dataPage = filteredData.Skip(request.Start).Take(request.Length)
                .Select(m => RecruitmentViewModel.ConvertRecruitmentViewModel(m));

            var response = DataTablesResponse.Create(request, data.Count(), filteredData.Count(), dataPage);

            return new DataTablesJsonResult(response, true);
        }

        [HttpGet]
        public IActionResult GetAreas(int? id)
        {
            var source = _areaApp.FindAll();
            var list = new List<Area>();
            source.ToList().ForEach(m =>
            {
                if (m.Path.Split(',').Length == 3)
                {
                    list.Add(m);
                }
            });
            var areas = (from o in list
                         select new NodeViewModel
                         {
                             id = o.Id,
                             text = o.Name,
                             parentId = o.ParentId,
                             path = o.Path,
                         }).ToList();
            var organizations = new List<NodeViewModel>();


            var areaId = _recruitment.GetAreaRecruits(id ?? 0);
            foreach (var org in areas)
            {
                org.state = areaId.Any(m => m.AreaId.Equals(org.id)) ? new State(false, false, true) : new State(false, false, false);


                organizations.Add(org);

            }

            return Json(organizations);
        }
        public IActionResult GetMarjor(int id)
        {
            var majors = _majorApp.Finds().Select(m => MajorViewModel.ConvertToViewModel(m)).ToList();
            var relation = _majorApp.Finds().Include(m => m.MajorRecruitMent)
                .Where(m => m.MajorRecruitMent.Any(n => n.RecruitMentId == id)).ToList();
            for (var i = 0; i < majors.Count(); i++)
            {
                if (relation.Any(m => m.Id.Equals(majors[i].Id)))
                {
                    majors[i].Checked = true;
                }
            }

            return Json(majors);
        }
    }
}