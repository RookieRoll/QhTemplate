using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QhTemplate.FontWeb.Models.BriefingContents
{
    public class BriefingContentViewModel
    {
        public int AreaId { get; set; }
        public List<BriefingContentList> Result { get; set; }
        public int MenuType { get; set; }
        public int Page { get; set; }
    }

    public class BriefingContentList
    {
        public string Company { get; set; }
        public string StartTime { get; set; }
        public string Held { get; set; }
        public string PublishTime { get; set; }
        public int Id { get; set; }
    }
}
