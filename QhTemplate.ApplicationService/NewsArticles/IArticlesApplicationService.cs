using System;
using System.Linq;
using QhTemplate.MysqlEntityFrameWorkCore.Models;

namespace QhTemplate.ApplicationService.NewsArticles
{
    public interface IArticlesApplicationService
    {
        IQueryable<NewArticle> Finds();
        IQueryable<NewArticle> Finds(Func<NewArticle, bool> func);
        NewArticle FirstOrDefault(Func<NewArticle, bool> func);
        NewArticle First(Func<NewArticle, bool> func);
        NewArticle Find(int id);
        void Create(string title, string content);
        void Remove(int id);
        void Update(NewArticle area);
    }

}