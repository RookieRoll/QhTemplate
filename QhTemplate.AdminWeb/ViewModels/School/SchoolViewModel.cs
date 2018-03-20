using Microsoft.Scripting.Runtime;
using QhTemplate.MysqlEntityFrameWorkCore.Models;

namespace QhTemplate.AdminWeb.ViewModels.School
{
    public class SchoolViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Address { get; set; }

        public static SchoolViewModel ConvertToViewModel(SchoolArea school)
        {
            return new SchoolViewModel
            {
                Id = school.Id,
                Name = school.Name,
                Code = school.Code
            };
        }
    }
}