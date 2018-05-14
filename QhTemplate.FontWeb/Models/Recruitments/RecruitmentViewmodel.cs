

using System.Collections.Generic;

namespace QhTemplate.FontWeb.Models.Recruitments
{
    public class RecruitmentViewmodel
    {
        public int MenuType { get; set; }
        public int Page { get; set; }
        public List<RecruitmentModel> Result { get; set; }

        public int TypeId { get; set; }

        public int MajorId { get; set; }
    }

    public class RecruitmentModel
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string JobName { get; set; }
        public string Email { get; set; }

    }

    public class AreaViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }
}