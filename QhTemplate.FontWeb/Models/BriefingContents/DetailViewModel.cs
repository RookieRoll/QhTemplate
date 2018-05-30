using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QhTemplate.FontWeb.Models.BriefingContents
{
    public class DetailViewModel
    {
        public int Id { get; set; }
        public string SchoolName { get; set; }
        public string CompanyName { get; set; }
        public string Held { get; set; }
        public string StartTime { get; set; }
        public string PublishTime { get; set; }
        public string Content { get; set; }

    }
}
