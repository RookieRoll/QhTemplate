using System.Resources;
using QhTemplate.MysqlEntityFrameWorkCore.Models;

namespace QhTemplate.AdminWeb.ViewModels.Areas
{
    public class AreasViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public static AreasViewModel ConvertAreasViewModel(Area area)
        {
            return new AreasViewModel
            {
                Id = area.Id,
                Name = area.Name,
                Code = area.Code
            };
        }
    }
}