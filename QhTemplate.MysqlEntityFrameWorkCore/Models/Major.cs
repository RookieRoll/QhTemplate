using System;
using System.Collections.Generic;

namespace QhTemplate.MysqlEntityFrameWorkCore.Models
{
    public partial class Major
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        
        public ICollection<MajorRecruitMent> MajorRecruitMent { get; set; }
    }
}
