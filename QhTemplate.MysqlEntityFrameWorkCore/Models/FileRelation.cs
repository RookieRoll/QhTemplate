using System;
using System.Collections.Generic;

namespace QhTemplate.MysqlEntityFrameWorkCore.Models
{
    public partial class FileRelation
    {
        public int CompanyId { get; set; }
        public int UserId { get; set; }
        public int RecruitId { get; set; }
        public string DisplayName { get; set; }
        public string RealName { get; set; }
        public DateTime? CreateTime { get; set; }
        public int Id { get; set; }
    }
}
