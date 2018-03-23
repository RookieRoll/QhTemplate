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
        

    }
}