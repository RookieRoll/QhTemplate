using System;
using System.Collections.Generic;

namespace QhTemplate.MysqlEntityFrameWorkCore.Models
{
    public partial class Organization
    {
        public Organization()
        {
            UserOrganization = new HashSet<UserOrganization>();
        }

        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public Boolean IsDeleted { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public DateTime? DeletionTime { get; set; }

        public ICollection<UserOrganization> UserOrganization { get; set; }

        public void Delete()
        {
            this.IsDeleted = true;
            this.DeletionTime=DateTime.Now;
            this.LastModificationTime=DateTime.Now;
        }
    }
}
