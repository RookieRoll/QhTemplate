using System;
using System.Linq;
using DataTables.AspNet.AspNetCore;
using DataTables.AspNet.Core;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Dynamic.Core;
using QhTemplate.AdminWeb.ViewModels.School;
using QhTemplate.ApplicationService.Schools;

namespace QhTemplate.AdminWeb.Controllers
{
    public class SchoolController : Controller
    {
        private readonly ISchoolService _schoolService;

        public SchoolController(ISchoolService schoolService)
        {
            _schoolService = schoolService;
        }

        public IActionResult Create(string name, string code, string address, string path, int areaid)
        {
            _schoolService.Create(name, code, path, address, areaid);
            return Json("创建成功");
        }

        public IActionResult Update(int id)
        {
            var school = _schoolService.Find(id);
            return PartialView("_Update");
        }

        public IActionResult Update()
        {
            _schoolService.Update();
            return Json("修改成功");
        }

        public IActionResult Delete(int id)
        {
            var school = _schoolService.Find(id);
            return PartialView("_Delete",);
        }

        [HttpPost]
        public IActionResult DeleteComfirm(int id)
        {
            _schoolService.Remove(id);
            return Json("删除成功");
        }
        public IActionResult GetData(IDataTablesRequest request, Guid areaId)
        {
            var data = _schoolService.Finds().Where(m => m.Path.Contains(areaId.ToString()));

            var filteredData = String.IsNullOrWhiteSpace(request.Search.Value)
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
                .Select(m => SchoolViewModel.ConvertToViewModel(m));

            var response = DataTablesResponse.Create(request, data.Count(), filteredData.Count(), dataPage);

            return new DataTablesJsonResult(response, true);
        }
    }
}