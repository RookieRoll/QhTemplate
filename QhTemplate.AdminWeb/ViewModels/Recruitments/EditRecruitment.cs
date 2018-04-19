using System;
using System.Collections.Generic;

namespace QhTemplate.AdminWeb.ViewModels.Recruitments
{
    public class EditRecruitment
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime EndTime { get; set; }
        public List<int> MajorIds { get; set; }
    }
}