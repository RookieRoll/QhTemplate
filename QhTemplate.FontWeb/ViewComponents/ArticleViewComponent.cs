using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QhTemplate.FontWeb.Models.Articles;
using QhTemplate.MysqlEntityFrameWorkCore.Models;

namespace QhTemplate.FontWeb.ViewComponents
{
    public class ArticleViewComponent:ViewComponent
    {
        private readonly EmsDBContext _context;

        public ArticleViewComponent(EmsDBContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            var result = _context.NewArticle.OrderByDescending(m => m.PublishTime).Take(40).Select(m => new ArticleViewModel
            {
                Id = m.Id,
                Time = m.PublishTime.ToString("yyyy-MM-dd HH-mm"),
                Title = m.Title,
                Content = m.SubContent
            });
            return View("_article",result);
        }
    }
}
