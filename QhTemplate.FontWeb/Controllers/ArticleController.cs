using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QhTemplate.ApplicationCore;
using QhTemplate.ApplicationCore.Exceptions;
using QhTemplate.FontWeb.Filer;
using QhTemplate.FontWeb.Models.Articles;
using QhTemplate.MysqlEntityFrameWorkCore.Models;

namespace QhTemplate.FontWeb.Controllers
{
    public class ArticleController : Controller
    {
        private readonly EmsDBContext _context;
        private const int Size = BaseConst.Size;

        public ArticleController(EmsDBContext context)
        {
            _context = context;
        }

        #region 首页新闻
        public IActionResult Index()
        {
            var result = _context.NewArticle.Where(m => m.SchoolId == null).OrderByDescending(m => m.PublishTime)
                .Take(Size).Select(m => new ArticleViewModel
                {
                    Id = m.Id,
                    Time = m.PublishTime.ToString("yyyy-MM-dd HH-mm"),
                    Title = m.Title,
                    Content = m.SubContent
                });
            return View(result);
        }

        public IActionResult Detail(int id)
        {
            var result = _context.NewArticle.FirstOrDefault(m => m.Id == id);
            var model = new ArticleViewModel
            {
                Id = result.Id,
                Content = result.Content,
                Time = result.PublishTime.ToString("yyyy-MM-dd HH:mm"),
                Title = result.Title
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult GetMore(int page = 1)
        {
            page -= 1;
            var total = _context.NewArticle.Count();
            var temp = _context.NewArticle.OrderByDescending(m => m.PublishTime);
            var result = _context.NewArticle.Where(m => m.SchoolId == null).OrderByDescending(m => m.PublishTime).Skip(page * Size).Take(Size).Select(
                m => new ArticleViewModel
                {
                    Id = m.Id,
                    Time = m.PublishTime.ToString("yyyy-MM-dd HH:mm"),
                    Title = m.Title,
                    Content = m.SubContent
                }).ToList();

            if ((page) * Size + result.Count() > total)
            {
                result = new List<ArticleViewModel>();
            }

            return PartialView("_ArticleView", result);
        }
        #endregion

        #region 学校新闻
        [MyAuthentications]
        public IActionResult SchoolArticle()
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(m => m.Type == ClaimTypes.Sid) ??
                         throw new UserFriendlyException("请登录");
            var id = int.Parse(userId.Value);
            var schoolId = _context.SchoolUser.FirstOrDefault(m => m.UserId == id);
            var artiles = _context.NewArticle.Where(m => m.SchoolId.Equals(schoolId))
                .OrderByDescending(m => m.PublishTime).Take(Size).Select(m => new ArticleViewModel
                {
                    Id = m.Id,
                    Time = m.PublishTime.ToString("yyyy-MM-dd HH-mm"),
                    Title = m.Title,
                    Content = m.SubContent
                });
            return View("SchoolArticle", artiles);
        }

        public IActionResult MoreSchoolArticle(int page = 1)
        {
            page -= 1;
            var userId = HttpContext.User.Claims.FirstOrDefault(m => m.Type == ClaimTypes.Sid) ??
                         throw new UserFriendlyException("请登录");
            var id = int.Parse(userId.Value);
            var schoolId = _context.SchoolUser.FirstOrDefault(m => m.UserId == id);
            var artiles = _context.NewArticle.Where(m => m.SchoolId.Equals(schoolId))
                .OrderByDescending(m => m.PublishTime).Skip(page * Size).Take(Size).Select(m => new ArticleViewModel
                {
                    Id = m.Id,
                    Time = m.PublishTime.ToString("yyyy-MM-dd HH-mm"),
                    Title = m.Title,
                    Content = m.SubContent
                });
            return PartialView("_SchoolArticle", artiles);
        }

        public IActionResult SchoolArticleDetail(int id)
        {
            var result = _context.NewArticle.FirstOrDefault(m => m.Id == id);
            var model = new ArticleViewModel
            {
                Id = result.Id,
                Content = result.Content,
                Time = result.PublishTime.ToString("yyyy-MM-dd HH:mm"),
                Title = result.Title
            };
            return View(model);
        }
        #endregion
    }
}