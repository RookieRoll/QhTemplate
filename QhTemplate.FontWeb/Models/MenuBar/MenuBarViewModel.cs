using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QhTemplate.FontWeb.Models.MenuBar
{
    public class MenuBar
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
    }

    public class MenuBarViewModel
    {
        public List<MenuBar> MenuBar { get; set; }
        public int AreaId { get; set; }
        public int MenuType { get; set; }
        public string UserName { get; set; }
    }
}
