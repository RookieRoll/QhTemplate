using System;
using System.Collections.Generic;

namespace QhTemplate.MysqlEntityFrameWorkCore.Models
{
    public partial class Recruitment
    {
        public Recruitment()
        {
            MajorRecruitMent = new HashSet<MajorRecruitMent>();
        }

        public int Id { get; set; }
        public int? CompanyId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime EndTime { get; set; }

        public ICollection<MajorRecruitMent> MajorRecruitMent { get; set; }

        public static Recruitment Create(string title, string content, DateTime endTime, int companyid)
        {
            return new Recruitment()
            {
                CompanyId = companyid,
                Title = title,
                Content = content,
                CreateTime = DateTime.Now,
                EndTime = endTime
            };
        }

        public void Update(string title, string content, DateTime endtime)
        {
            this.Title = title;
            this.Content = content;
            this.EndTime = endtime;
        }
    }
}
