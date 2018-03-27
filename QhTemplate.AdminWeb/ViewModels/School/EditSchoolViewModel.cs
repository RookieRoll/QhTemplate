using System.Collections.Generic;
using QhTemplate.MysqlEntityFrameWorkCore.Models;

namespace QhTemplate.AdminWeb.ViewModels.School
{
    public class EditSchoolViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Provice { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Address { get; set; }

        public static EditSchoolViewModel ConvertSchoolViewModel(SchoolArea school)
        {
            var path = school.Path.Split("-");
            return new EditSchoolViewModel()
            {
                Id = school.Id,
                Name = school.Name,
                Code = school.Name,
                Provice = path[0],
                City = path[1],
                District = path[2],
                Address = school.Address
            };
        }
    }
}