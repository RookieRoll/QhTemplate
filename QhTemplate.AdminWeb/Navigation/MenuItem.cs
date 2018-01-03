using System.Collections.Generic;

namespace QhTemplate.AdminWeb.Navigation
{
    public class MenuItem
    {
        public MenuItem()
        {
            Children = new List<MenuItem>();
        }
        
        public string RequiredAuthorizeCode { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public List<MenuItem> Children { get; set; }

        public bool HasChild => Children.Count > 0;
    }
}