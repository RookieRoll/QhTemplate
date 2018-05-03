using System;
using System.Collections.Generic;
using QhTemplate.MysqlEntityFrameWorkCore.Models;

namespace QhTemplate.AdminWeb.ViewModels.Recruitments
{
    public class EditRecruitment
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string EndTime { get; set; }
        public List<int> MajorIds { get; set; }

        public static EditRecruitment Convert(Recruitment recruit)
        {
            return new EditRecruitment()
            {
                Id=recruit.Id,
                Title=recruit.Title,
                Content=recruit.Content,
                EndTime=recruit.EndTime.ToString("yyyy-MM-dd")
            };
        }
    }
}