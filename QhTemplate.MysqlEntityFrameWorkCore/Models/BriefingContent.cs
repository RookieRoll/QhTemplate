using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace QhTemplate.MysqlEntityFrameWorkCore.Models
{
    public partial class BriefingContent
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public int? SchoolId { get; set; }
        public string Held { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string OpthonList { get; set; }
        public DateTime PublishTime { get; set; }
        public DateTime StartTime { get; set; }

        public static BriefingContent Create(string title, string content, string held,
            DateTime startTime, int schoolId)
        {
            return new BriefingContent
            {
                Held = held,
                SchoolId = schoolId,
                Title = title,
                Content = content,
                StartTime = DateTime.Now,
                PublishTime = DateTime.Now,
            };
        }

        public void Update(string title, string content, string held,
            DateTime startTime)
        {
            this.Content = content;
            this.Title = title;
            this.StartTime = startTime;
            this.Held = held;
        }
    }
}
