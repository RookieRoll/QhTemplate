using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace QhTemplate.MysqlEntityFrameWorkCore.Models
{
    public partial class Role
    {
        public Role()
        {
            UserRole = new HashSet<UserRole>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public Boolean IsStatic { get; set; }
        public Boolean IsDefault { get; set; }
        public Boolean IsDeleted { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public DateTime? DeletionTime { get; set; }

        public ICollection<UserRole> UserRole { get; set; }

        public static Role Create(string name,bool isDefault)
        {
            return new Role()
            {
                Name = name,
                IsStatic = false,
                IsDefault = isDefault,
                IsDeleted = false,
                CreationTime = DateTime.Now,
                LastModificationTime = DateTime.Now
            };
        }

        public void Update(string roleName, bool isDefault)
        {
            this.Name = roleName;
            this.IsDefault = isDefault;
            this.LastModificationTime=DateTime.Now;
        }

        public void Delete()
        {
            this.DeletionTime=DateTime.Now;
            this.IsDeleted = true;
            this.LastModificationTime=DateTime.Now;
        }
    }
}