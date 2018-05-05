using System;
using System.Linq;
using DataTables.AspNet.AspNetCore;
using DataTables.AspNet.Core;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Dynamic.Core;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using QhTemplate.AdminWeb.ViewModels.BriefingContents;
using QhTemplate.ApplicationService.BriefingContents;
using QhTemplate.ApplicationService.Schools;
using QhTemplate.ApplicationService.Utils;
using QhTemplate.MysqlEntityFrameWorkCore.Models;


namespace QhTemplate.AdminWeb.Controllers
{
    public class CareerTalkController : Controller
    {
        private readonly IBriefingContentService _briefingContent;
        private readonly ISchoolService _schoolService;
        public CareerTalkController(IBriefingContentService briefingContent, ISchoolService schoolService)
        {
            _briefingContent = briefingContent;
            _schoolService = schoolService;
        }

        // GET
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        public IActionResult Create(EditBriefContentViewModel obj)
        {
            var userId = HttpContext.User.Claims.SingleOrDefault(x => x.Type.Equals(ClaimTypes.Sid))?.Value;
            var id = int.Parse(userId);
            var school = _schoolService.Finds().Include(m => m.SchoolUser).First(m => m.SchoolUser.Any(n => n.UserId == id));
            _briefingContent.Create(obj.Title, obj.Content, obj.Held, DateTime.Parse(obj.StartTime), 16, obj.CompanyName);
            return Json("创建成功");
        }

        public IActionResult Update(int id)
        {
            var career = _briefingContent.Find(id);
            return View("Update", EditBriefContentViewModel.ConvertToEditViewModel(career));
        }

        [HttpPost]
        public IActionResult Update(EditBriefContentViewModel obj)
        {
            var brefing = new BriefingContent()
            {
                Id = obj.Id,
                Held = obj.Held,
                Title = obj.Title,
                StartTime = DateTime.Parse(obj.StartTime),
                CompanyName = obj.CompanyName,
                Content = obj.Content
            };
            _briefingContent.Update(brefing);
            return Json("修改成功");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var career = _briefingContent.Find(id);
            return PartialView("_Delete", BriefingContentViewModel.ConvertToViewModel(career));
        }

        [HttpPost]
        public IActionResult DeleteComfirm(int id)
        {
            _briefingContent.Delete(id);
            return Json("删除成功");
        }

        public IActionResult GetData(IDataTablesRequest request)
        {
            var data = _briefingContent.Finds();

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
                filteredData = filteredData.OrderByDescending(m => m.PublishTime);
            }

            var dataPage = filteredData.Skip(request.Start).Take(request.Length)
                .Select(m => BriefingContentViewModel.ConvertToViewModel(m));

            var response = DataTablesResponse.Create(request, data.Count(), filteredData.Count(), dataPage);

            return new DataTablesJsonResult(response, true);
        }
    }
}