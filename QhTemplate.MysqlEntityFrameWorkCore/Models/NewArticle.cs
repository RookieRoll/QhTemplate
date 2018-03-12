using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;

namespace QhTemplate.MysqlEntityFrameWorkCore.Models
{
    public partial class NewArticle
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PublishTime { get; set; }
        public bool IsDelete { get; set; }
        public string SubContent { get; set; }
        
        public static NewArticle Create(string title, string content,string subcontent)
        {
            return new NewArticle()
            {
                Title = title,
                Content = content,
                PublishTime = DateTime.Now,
                IsDelete = false,
                SubContent = subcontent
            };
        }

        public void Update(string title, string content,string subcontent)
        {
            this.Title = title;
            this.Content = content;
            this.SubContent = subcontent;
        }
        public void Delete()
        {
            this.IsDelete = true;
        }
    }
}
