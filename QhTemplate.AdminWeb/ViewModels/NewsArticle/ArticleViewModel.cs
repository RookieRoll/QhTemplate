using QhTemplate.MysqlEntityFrameWorkCore.Models;

namespace QhTemplate.AdminWeb.ViewModels.NewsArticle
{
    public class ArticleViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string SubContent { get; set; }
        public string Time { get; set; }

        public static ArticleViewModel ConvertToViewModel(NewArticle article)
        {
            return new ArticleViewModel
            {
                Id = article.Id,
                Title = article.Title,
                SubContent =
                    article.SubContent.Length > 100 ? article.SubContent.Substring(0, 100) : article.SubContent,
                Time = article.PublishTime.ToString("yyyy-MM-dd")
            };
        }
    }
}