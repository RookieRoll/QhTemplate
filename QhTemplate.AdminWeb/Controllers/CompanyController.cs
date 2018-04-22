using Microsoft.AspNetCore.Mvc;
using QhTemplate.ApplicationService.Companys;
using System.Linq.Dynamic.Core;
using DataTables.AspNet.AspNetCore;
using DataTables.AspNet.Core;
using System.Linq;
using QhTemplate.AdminWeb.ViewModels.Company;

namespace QhTemplate.AdminWeb.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        // GET
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Delete(int id)
        {
            var company = _companyService.Find(id);
            
            return PartialView("_Delete",CompanyViewModel.ConvertCompanyViewModel(company));
        }

        public IActionResult DeleteConfirm(int id)
        {
            _companyService.Delete(id);
            return Json("删除成功");
        }

        public IActionResult GetData(IDataTablesRequest request)
        {
            var data = _companyService.Finds();

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
                .Select(m => CompanyViewModel.ConvertCompanyViewModel(m));

            var response = DataTablesResponse.Create(request, data.Count(), filteredData.Count(), dataPage);

            return new DataTablesJsonResult(response, true);
        }
    }
}