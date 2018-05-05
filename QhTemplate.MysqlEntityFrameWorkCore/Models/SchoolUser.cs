using System;
using System.Collections.Generic;

namespace QhTemplate.MysqlEntityFrameWorkCore.Models
{
    public partial class SchoolUser
    {
        public int SchoolId { get; set; }
        public int UserId { get; set; }

        public SchoolArea School { get; set; }
        public User User { get; set; }
    }
}
