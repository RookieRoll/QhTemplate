using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QhTemplate.FontWeb.Models.NoticeBrefing
{
    public class NoticeBriefingViewModel
    {
        public int Id { get; set; }
        public int BriefId { get; set; }
        public string Company { get; set; }
        public string Time { get; set; }
        public string PublishTime { get; set; }
        public string Held { get; set; }
        public bool IsExpired { get; set; }

    }
}
