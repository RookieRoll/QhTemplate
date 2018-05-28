using System.Collections.Generic;

namespace QhTemplate.FontWeb.Models.NoticeBrefing
{
    public class Notice
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public List<BriefingMessage> Briefings;
    }

    public class BriefingMessage
    {
        public int Id { get; set; }
        public string Company { get; set; }
        public string Held { get; set; }
        public string Time { get; set; }
    }
}