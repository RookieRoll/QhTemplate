using System;
using System.Collections.Generic;

namespace QhTemplate.MysqlEntityFrameWorkCore.Models
{
    public partial class MajorRecruitMent
    {
        public int MajorId { get; set; }
        public int RecruitMentId { get; set; }

        public Major Major { get; set; }
        public Recruitment RecruitMent { get; set; }
    }
}
