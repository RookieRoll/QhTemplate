using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QhTemplate.ApplicationCore;
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

        public IActionResult Index()
        {
            var result = _context.NewArticle.OrderByDescending(m => m.PublishTime).Take(Size).Select(m => new ArticleViewModel
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
            var result = _context.NewArticle.OrderByDescending(m => m.PublishTime).Skip(page*Size).Take(Size).Select(m => new ArticleViewModel
            {
                Id = m.Id,
                Time = m.PublishTime.ToString("yyyy-MM-dd HH:mm"),
                Title = m.Title,
                Content = m.SubContent
            }).ToList();

            if((page)* Size + result.Count() > total)
            {
                result = new List<ArticleViewModel>();
            }

            return PartialView("_ArticleView", result);
        }
    }
}