using System.Collections.Generic;
using QhTemplate.MysqlEntityFrameWorkCore.Models;

namespace QhTemplate.AdminWeb.ViewModels.School
{
    public class EditSchoolViewModel
    {
        public int Id { get; set; }
        public int AreaId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Address { get; set; }

        public static EditSchoolViewModel ConvertSchoolViewModel(SchoolArea school)
        {
            return new EditSchoolViewModel()
            {
                AreaId=school.AreaId,
                Id = school.Id,
                Name = school.Name,
                Code = school.Code,
                Address = school.Address
            };
        }
    }
}