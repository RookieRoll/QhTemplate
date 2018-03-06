using System;
using System.Linq;
using QhTemplate.ApplicationCore.News;
using QhTemplate.MysqlEntityFrameWorkCore.Models;

namespace QhTemplate.ApplicationService.NewsArticles
{
    public class ArticlesApplicationService : IArticlesApplicationService
    {
        private readonly ArticleManagers _article;

        public ArticlesApplicationService(ArticleManagers article)
        {
            _article = article;
        }

        public IQueryable<NewArticle> Finds()
        {
            return _article.Finds().AsQueryable();
        }

        public IQueryable<NewArticle> Finds(Func<NewArticle, bool> func)
        {
            return _article.Finds(func).AsQueryable();
        }

        public NewArticle FirstOrDefault(Func<NewArticle, bool> func)
        {
            return _article.FirstOrDefault(func);
        }

        public NewArticle First(Func<NewArticle, bool> func)
        {
            return _article.First(func);
        }

        public NewArticle Find(int id)
        {
            return _article.Find(id);
        }

        public void Create(string title, string content,string subcontent)
        {
            var article = NewArticle.Create(title, content,subcontent);
            _article.Create(article);
        }

        public void Remove(int id)
        {
            _article.Deleted(id);
        }

        public void Update(NewArticle area)
        {
           _article.Update(area);
        }
    }
}