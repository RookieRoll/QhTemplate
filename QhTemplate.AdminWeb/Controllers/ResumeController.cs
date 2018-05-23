using System;
using System.Linq;
using DataTables.AspNet.AspNetCore;
using DataTables.AspNet.Core;
using Microsoft.AspNetCore.Mvc;
using QhTemplate.AdminWeb.ViewModels.Resumes;
using QhTemplate.ApplicationService.Resumes;
using QhTemplate.ApplicationService.Users;
using System.Linq.Dynamic.Core;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using QhTemplate.AdminWeb.Filter;
using QhTemplate.ApplicationService.Companys;
using QhTemplate.MysqlEntityFrameWorkCore.Models;

namespace QhTemplate.AdminWeb.Controllers
{
    [MyAuthentications]
    public class ResumeController : Controller
    {
        private readonly IResumeService _resumeService;

        private readonly IUserAppService _appService;
        private readonly ICompanyService _companyService;

        public ResumeController(IResumeService resumeService, IUserAppService appService,
            ICompanyService companyService)
        {
            _resumeService = resumeService;
            _appService = appService;
            _companyService = companyService;
        }

        // GET
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Delete(int id)
        {
            var resume = _resumeService.Find(id);
            return PartialView("_Delete", ResumeViewModel.ConvertToDelete(resume));
        }

        [HttpPost]
        public IActionResult DeleteComfirm(int id)
        {
            _resumeService.DeleteComfirm(id);
            return Json("删除成功");
        }

        public IActionResult GetData(IDataTablesRequest request)
        {
            var tempid = HttpContext.User.Claims.SingleOrDefault(x => x.Type.Equals(ClaimTypes.Sid))?.Value;
            int id = int.Parse(tempid);
            int companyId = _companyService.Finds().Include(m => m.CompanyUser)
                .FirstOrDefault(m => m.CompanyUser.Any(n => n.UserId == id)).Id;
            var data = (from file in _resumeService.Finds()
                join user in _appService.Finds() on file.UserId equals user.Id
                where file.CompanyId == companyId && !file.IsDeleted
                select new ResumeViewModel
                {
                    Id = file.Id,
                    SendTime = ((DateTime) file.CreateTime).ToString("yyyy-MM-dd"),
                    FileName = file.DisplayName,
                    UserEmail = user.EmailAddress,
                    UserName = user.Name,
                    CompanyId = file.CompanyId,
                    UserId = file.UserId,
                    Status = file.Status == 0 ? "未读" : "已读"
                });

            var filteredData = string.IsNullOrWhiteSpace(request.Search.Value)
                ? data
                : data.Where(item => item.FileName.Contains(request.Search.Value));

            var sortColumn = request.Columns.FirstOrDefault(c => c.Sort != null);
            if (sortColumn != null)
            {
                string ascending = sortColumn.Sort.Direction == SortDirection.Ascending ? "asc" : "desc";
                string orderStr = $"{sortColumn.Field} {ascending}";
                filteredData = filteredData.OrderBy(orderStr);
            }

            var dataPage = filteredData.Skip(request.Start).Take(request.Length);

            var response = DataTablesResponse.Create(request, data.Count(), filteredData.Count(), dataPage);

            return new DataTablesJsonResult(response, true);
        }
    }
}