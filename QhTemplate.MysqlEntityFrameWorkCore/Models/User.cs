using System;
using System.Collections.Generic;

namespace QhTemplate.MysqlEntityFrameWorkCore.Models
{
    public partial class User
    {
        public User()
        {
            UserOrganization = new HashSet<UserOrganization>();
            UserRole = new HashSet<UserRole>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string EmailAddress { get; set; }
        public Boolean IsDeleted { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public DateTime? DeletionTime { get; set; }

        public ICollection<UserOrganization> UserOrganization { get; set; }
        public ICollection<UserRole> UserRole { get; set; }
    }
}
