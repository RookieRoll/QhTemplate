using System.Linq;
using DataTables.AspNet.AspNetCore;
using DataTables.AspNet.Core;
using Microsoft.AspNetCore.Mvc;
using QhTemplate.AdminWeb.ViewModels.Major;
using QhTemplate.ApplicationService.NewsArticles;
using System.Linq.Dynamic.Core;
using QhTemplate.AdminWeb.ViewModels.NewsArticle;


namespace QhTemplate.AdminWeb.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly IArticlesApplicationService _applicationService;

        public ArticlesController(IArticlesApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        // GET
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            
        }
        public IActionResult Update()
        {
            
        }

        [HttpPost]
        public IActionResult Update(CreateArticleViewModel model)
        {
            
        }

        public IActionResult Delete(int id)
        {
            
        }

        [HttpPost]
        public IActionResult DeleteComfirm(int id)
        {
            
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

            var dataPage = filteredData.Skip(request.Start).Take(request.Length)
                .Select(m => ArticleViewModel.ConvertToViewModel(m));

            var response = DataTablesResponse.Create(request, data.Count(), filteredData.Count(), dataPage);

            return new DataTablesJsonResult(response, true);
        }
    }
}