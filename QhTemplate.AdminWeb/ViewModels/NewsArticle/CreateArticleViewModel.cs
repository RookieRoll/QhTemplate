using QhTemplate.MysqlEntityFrameWorkCore.Models;

namespace QhTemplate.AdminWeb.ViewModels.NewsArticle
{
    public class CreateArticleViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string SubContent { get; set; }
        public string Content { get; set; }
    }
}