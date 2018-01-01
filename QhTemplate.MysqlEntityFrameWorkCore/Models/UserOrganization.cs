using System;
using System.Collections.Generic;

namespace QhTemplate.MysqlEntityFrameWorkCore.Models
{
    public partial class UserOrganization
    {
        public int UserId { get; set; }
        public int OrganizationId { get; set; }

        public Organization Organization { get; set; }
        public User User { get; set; }
    }
}
