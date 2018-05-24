using QhTemplate.MysqlEntityFrameWorkCore.Models;

namespace QhTemplate.AdminWeb.ViewModels.NewsArticle
{
    public class ArticleViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string PublishTime { get; set; }

        public static ArticleViewModel ConvertToViewModel(NewArticle article)
        {
            return new ArticleViewModel
            {
                Id = article.Id,
                Title = article.Title,
                Summary =
                    article.SubContent.Length > 50 ? article.SubContent.Substring(0, 50) : article.SubContent,
                PublishTime = article.PublishTime.ToString("yyyy-MM-dd")
            };
        }
    }
}