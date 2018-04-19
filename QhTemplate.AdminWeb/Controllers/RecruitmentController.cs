using System;
using System.Linq;
using DataTables.AspNet.AspNetCore;
using DataTables.AspNet.Core;
using Microsoft.AspNetCore.Mvc;
using QhTemplate.ApplicationService.Recruitments;
using System.Linq.Dynamic.Core;
using System.Security.Claims;
using Community.CsharpSqlite;
using QhTemplate.AdminWeb.ViewModels.Recruitments;
using QhTemplate.ApplicationService.Companys;
using QhTemplate.MysqlEntityFrameWorkCore.Models;

namespace QhTemplate.AdminWeb.Controllers
{
    public class RecruitmentController : Controller
    {
        private readonly IRecruitmentServcie _recruitment;
        private readonly ICompanyService _company;
        public RecruitmentController(IRecruitmentServcie recruitment, ICompanyService company)
        {
            _recruitment = recruitment;
            _company = company;
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
            var userId =HttpContext.User.Claims.SingleOrDefault(x => x.Type.Equals(ClaimTypes.Sid))?.Value;
            var id = int.Parse(userId);
            var companyId = _company.Finds(m => m.CompanyUser.Any(n => n.UserId == id)).First().Id;
            _recruitment.Create(obj.Title,obj.Content,obj.EndTime,id,obj.MajorIds);
            return Json("创建成功");
        }

        public IActionResult Update(int id)
        {
            
            return View("Update");
        }

        [HttpPost]
        public IActionResult Update(EditRecruitment obj)
        {
            var temp = new Recruitment
            {
                Id = obj.Id,
                Title = obj.Title,
                Content = obj.Content,
                EndTime = obj.EndTime,

            };
            _recruitment.Update(temp,obj.MajorIds);
            return Json("修改成功");
        }

        public IActionResult Delete(int id)
        {
            var temp = _recruitment.Find(id);
            return PartialView("_Delete",RecruitmentViewModel.ConvertRecruitmentViewModel(temp));
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
                filteredData=filteredData.OrderByDescending(m => m.CreateTime).ThenBy(m=>m.EndTime);
            }

            var dataPage = filteredData.Skip(request.Start).Take(request.Length)
                .Select(m => RecruitmentViewModel.ConvertRecruitmentViewModel(m));

            var response = DataTablesResponse.Create(request, data.Count(), filteredData.Count(), dataPage);

            return new DataTablesJsonResult(response, true);
        }
    }
}