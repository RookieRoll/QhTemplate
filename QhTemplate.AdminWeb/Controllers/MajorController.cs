using System.Linq;
using System.Linq.Dynamic.Core;
using DataTables.AspNet.AspNetCore;
using DataTables.AspNet.Core;
using Microsoft.AspNetCore.Mvc;
using QhTemplate.AdminWeb.ViewModels.Major;
using QhTemplate.ApplicationService.Majors;
using QhTemplate.MysqlEntityFrameWorkCore.Models;

namespace QhTemplate.AdminWeb.Controllers
{
    public class MajorController : Controller
    {
        private readonly IMajorAppService _majorApp;

        public MajorController(IMajorAppService majorApp)
        {
            _majorApp = majorApp;
        }

        // GET
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create(MajorViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Name) || string.IsNullOrWhiteSpace(model.Code))
                return Json("不能为空");
            _majorApp.Create(model.Name, model.Code);
            return Json("创建成功");
        }

        public IActionResult Update(int id)
        {
            var major = _majorApp.Find(id);
            return PartialView("_Update", MajorViewModel.ConvertToViewModel(major));
        }

        [HttpPost]
        public IActionResult Update(MajorViewModel model)
        {
            Major major = new Major()
            {
                Id = model.Id,
                Name = model.Name,
                Code = model.Code
            };
            _majorApp.Update(major);
            return Json("修改成功");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var major = _majorApp.Find(id);
            return PartialView("_Delete", MajorViewModel.ConvertToViewModel(major));
        }

        public IActionResult DeleteComfirm(int id)
        {
            _majorApp.Remove(id);
            return Json("删除成功");
        }
        public IActionResult GetData(IDataTablesRequest request)
        {
            var data = _majorApp.Finds();

            var filteredData = string.IsNullOrWhiteSpace(request.Search.Value)
                ? data
                : data.Where(item => item.Name.Contains(request.Search.Value));

            var sortColumn = request.Columns.FirstOrDefault(c => c.Sort != null);
            if (sortColumn != null)
            {
                string ascending = sortColumn.Sort.Direction == SortDirection.Ascending ? "asc" : "desc";
                string orderStr = $"{sortColumn.Field} {ascending}";
                filteredData = filteredData.OrderBy(orderStr);
            }

            var dataPage = filteredData.Skip(request.Start).Take(request.Length)
                .Select(m => MajorViewModel.ConvertToViewModel(m));

            var response = DataTablesResponse.Create(request, data.Count(), filteredData.Count(), dataPage);

            return new DataTablesJsonResult(response, true);
        }
    }
}