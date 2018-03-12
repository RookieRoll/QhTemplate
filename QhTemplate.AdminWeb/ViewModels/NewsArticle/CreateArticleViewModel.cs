using System.Resources;
using QhTemplate.MysqlEntityFrameWorkCore.Models;

namespace QhTemplate.AdminWeb.ViewModels.NewsArticle
{
    public class CreateArticleViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string SubContent { get; set; }
        public string Content { get; set; }

        public static CreateArticleViewModel ConvertToView(NewArticle article)
        {
            return new CreateArticleViewModel()
            {
                Id = article.Id,
                SubContent = article.SubContent,
                Content = article.Content,
                Title = article.Title
            };
        }
    }
}