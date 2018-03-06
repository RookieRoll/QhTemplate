using System.Net.NetworkInformation;
using Newtonsoft.Json.Linq;
using QhTemplate.ApplicationCore.Exceptions;
using QhTemplate.MysqlEntityFrameWorkCore.Models;

namespace QhTemplate.ApplicationCore.News
{
    public class ArticleManagers:BaseManager<NewArticle>
    {
        public ArticleManagers(EmsDBContext db) : base(db)
        {
        }

        public NewArticle Find(int id)
        {
            var article = FirstOrDefault(m => m.Id == id);
            return article ?? throw new UserFriendlyException("该文章不存在");
        }

        public void Create(NewArticle article)
        {
            _db.NewArticle.Add(article);
            Save();
        }

        public void Update(NewArticle article)
        {
            var news = Find(article.Id);
            news.Update(article.Title,article.Content,article.SubContent);
            Save();
        }

        public void Deleted(int id)
        {
            var news = Find(id);
            news.Delete();
            Save();
        }
        
    }
}