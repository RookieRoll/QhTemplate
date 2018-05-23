using System.Linq;
using DataTables.AspNet.AspNetCore;
using DataTables.AspNet.Core;
using Microsoft.AspNetCore.Mvc;
using QhTemplate.ApplicationService.NewsArticles;
using System.Linq.Dynamic.Core;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using QhTemplate.AdminWeb.Filter;
using QhTemplate.AdminWeb.ViewModels.NewsArticle;
using QhTemplate.MysqlEntityFrameWorkCore.Models;
using UEditorNetCore;
using QhTemplate.AdminWeb.Utils;
using QhTemplate.ApplicationService.Schools;

namespace QhTemplate.AdminWeb.Controllers
{
    [MyAuthentications]
    public class ArticlesController : Controller
    {
        private readonly IArticlesApplicationService _applicationService;
        private readonly UEditorService _uEditorService;
        private readonly ISchoolService _schoolService;
        public ArticlesController(IArticlesApplicationService applicationService, UEditorService uEditorService, ISchoolService schoolService)
        {
            _applicationService = applicationService;
            _uEditorService = uEditorService;
            _schoolService = schoolService;
        }

        // GET
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View("_Create");
        }
        [HttpPost]
        public IActionResult Create(CreateArticleViewModel model)
        {
            var usertype = HttpContext.User.Claims.SingleOrDefault(x => x.Type.Equals(ClaimTypes.Role))?.Value;
            var userId = HttpContext.User.Claims.SingleOrDefault(x => x.Type.Equals(ClaimTypes.Sid))?.Value;
            var id = int.Parse(userId);
            var schoolid = _schoolService.Finds().Include(m => m.SchoolUser)
                .FirstOrDefault(m => m.SchoolUser.Any(n => n.UserId == id))?.Id;
            var type = int.Parse(usertype);
            _applicationService.Create(model.Title, PathUtil.PathReplace(model.Content), model.SubContent,
                type == (int) UserType.Teacher ? schoolid : null);

            return Json("创建成功");
        }
        public IActionResult Update(int id)
        {
            var article = _applicationService.Find(id);
            var result = CreateArticleViewModel.ConvertToView(article);
            return View("_Update",result);
        }

        [HttpPost]
        public IActionResult Update(CreateArticleViewModel model)
        {
            NewArticle article=new NewArticle
            {
                Id = model.Id,
                Title = model.Title,
                Content = PathUtil.PathReplace(model.Content),
                SubContent = model.SubContent
            };
            _applicationService.Update(article);
            return Json("修改成功");
        }

        public IActionResult Delete(int id)
        {
            var article = _applicationService.Find(id);
            return PartialView("_Delete", ArticleViewModel.ConvertToViewModel(article));
        }

        [HttpPost]
        public IActionResult DeleteComfirm(int id)
        {
            _applicationService.Remove(id);
            return Json("创建成功");
        }

        public void DoUeditor()
        {
           _uEditorService.DoAction(HttpContext);
        }
        public IActionResult GetData(IDataTablesRequest request)
        {
            var data = _applicationService.Finds(m=>m.SchoolId==null);
            var usertype = HttpContext.User.Claims.SingleOrDefault(x => x.Type.Equals(ClaimTypes.Role))?.Value;
            var type = int.Parse(usertype);
            
            var userId = HttpContext.User.Claims.SingleOrDefault(x => x.Type.Equals(ClaimTypes.Sid))?.Value;
            var id = int.Parse(userId);
            var schoolid = _schoolService.Finds().Include(m => m.SchoolUser)
                .FirstOrDefault(m => m.SchoolUser.Any(n => n.UserId == id))?.Id;
            if (type == (int) UserType.Teacher)
            {
                data = _applicationService.Finds(m=>m.SchoolId==schoolid);
            }
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
                filteredData=filteredData.OrderByDescending(m => m.PublishTime);
            }

            var dataPage = filteredData.Skip(request.Start).Take(request.Length)
                .Select(m => ArticleViewModel.ConvertToViewModel(m));

            var response = DataTablesResponse.Create(request, data.Count(), filteredData.Count(), dataPage);

            return new DataTablesJsonResult(response, true);
        }
    }
}