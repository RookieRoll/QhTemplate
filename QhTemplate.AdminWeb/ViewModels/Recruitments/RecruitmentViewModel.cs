using System;
using QhTemplate.ApplicationService.Utils;
using QhTemplate.MysqlEntityFrameWorkCore.Models;

namespace QhTemplate.AdminWeb.ViewModels.Recruitments
{
    public class RecruitmentViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime EndTime { get; set; }

        public static RecruitmentViewModel ConvertRecruitmentViewModel(Recruitment recruitment)
        {
            return MapperUtil<Recruitment, RecruitmentViewModel>.Convert(recruitment);
        }
    }


}