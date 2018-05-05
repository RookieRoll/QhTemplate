using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Community.CsharpSqlite;
using QhTemplate.MysqlEntityFrameWorkCore.Models;

namespace QhTemplate.AdminWeb.ViewModels.BriefingContents
{
    public class BriefingContentViewModel
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string Held { get; set; }
        public string Title { get; set; }
        public string StartTime { get; set; }
        public string PublishTime { get; set; }

        public static BriefingContentViewModel ConvertToViewModel(BriefingContent content)
        {
            return new BriefingContentViewModel
            {
                Id = content.Id,
                CompanyName = content.CompanyName,
                Held = content.Held,
                Title = content.Title,
                StartTime = content.StartTime.ToString("yyyy-MM-dd hh:mm"),
                PublishTime = content.PublishTime.ToString("yyyy-MM-dd hh:mm")
            };
        }
    }
}
