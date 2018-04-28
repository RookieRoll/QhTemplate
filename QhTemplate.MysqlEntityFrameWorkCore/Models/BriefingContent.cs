using System;
using System.Collections.Generic;

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
        public DateTime? PublishTime { get; set; }
        public DateTime? StartTime { get; set; }
    }
}
