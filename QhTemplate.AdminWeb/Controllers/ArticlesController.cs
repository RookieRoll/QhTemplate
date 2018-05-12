using System.Linq;
using DataTables.AspNet.AspNetCore;
using DataTables.AspNet.Core;
using Microsoft.AspNetCore.Mvc;
using QhTemplate.ApplicationService.NewsArticles;
using System.Linq.Dynamic.Core;
using QhTemplate.AdminWeb.Filter;
using QhTemplate.AdminWeb.ViewModels.NewsArticle;
using QhTemplate.MysqlEntityFrameWorkCore.Models;
using UEditorNetCore;
using QhTemplate.AdminWeb.Utils;

namespace QhTemplate.AdminWeb.Controllers
{
    [MyAuthentications]
    public class ArticlesController : Controller
    {
        private readonly IArticlesApplicationService _applicationService;
        private readonly UEditorService _uEditorService;
        
        public ArticlesController(IArticlesApplicationService applicationService, UEditorService uEditorService)
        {
            _applicationService = applicationService;
            _uEditorService = uEditorService;
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
            _applicationService.Create(model.Title, PathUtil.PathReplace(model.Content),model.SubContent);
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
            var data = _applicationService.Finds();

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