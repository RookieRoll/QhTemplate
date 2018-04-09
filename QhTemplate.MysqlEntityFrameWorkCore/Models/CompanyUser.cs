using System;
using System.Collections.Generic;

namespace QhTemplate.MysqlEntityFrameWorkCore.Models
{
    public partial class CompanyUser
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CompanyId { get; set; }

        public Company Company { get; set; }
        public User User { get; set; }
    }
}
