using System;
using System.Collections.Generic;

namespace QhTemplate.MysqlEntityFrameWorkCore.Models
{
    public partial class Permission
    {
        public int Id { get; set; }
        public int? RoleId { get; set; }
        public int? UserId { get; set; }
        public string Code { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
