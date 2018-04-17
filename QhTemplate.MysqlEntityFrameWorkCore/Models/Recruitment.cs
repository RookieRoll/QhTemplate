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
        public DateTime? CreateTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int? MajorId { get; set; }

        public ICollection<MajorRecruitMent> MajorRecruitMent { get; set; }
    }
}
