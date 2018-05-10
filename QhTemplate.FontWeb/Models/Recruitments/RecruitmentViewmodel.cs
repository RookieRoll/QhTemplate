

using System.Collections.Generic;

namespace QhTemplate.FontWeb.Models.Recruitments
{
    public class RecruitmentViewmodel
    {
        public int MenuType { get; set; }
        public int Page { get; set; }
        public List<RecruitmentModel> Result { get; set; }
    }

    public class RecruitmentModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }
}