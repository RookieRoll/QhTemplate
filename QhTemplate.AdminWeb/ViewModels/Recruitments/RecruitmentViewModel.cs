using System;
using QhTemplate.ApplicationService.Utils;
using QhTemplate.MysqlEntityFrameWorkCore.Models;

namespace QhTemplate.AdminWeb.ViewModels.Recruitments
{
    public class RecruitmentViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string CreateTime { get; set; }
        public string EndTime { get; set; }

        public static RecruitmentViewModel ConvertRecruitmentViewModel(Recruitment recruitment)
        {
            return new RecruitmentViewModel
            {
                Id=recruitment.Id,
                Title=recruitment.Title,
                CreateTime=recruitment.CreateTime.ToString("yyyy-MM-dd"),
                EndTime = recruitment.EndTime.ToString("yyyy-MM-dd")
            };
        }
    }
}