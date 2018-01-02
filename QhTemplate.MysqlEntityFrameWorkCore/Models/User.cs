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

        public static User Create(string name, string password, string username, string email)
        {
            return new User()
            {
                Name = name,
                UserName = username,
                Password = password,
                EmailAddress = email,
                IsDeleted = false,
                CreationTime = DateTime.Now,
                LastModificationTime = DateTime.Now
            };
        }

        public void Update(string username, string email)
        {
            this.UserName = username;
            this.EmailAddress = email;
            this.LastModificationTime = DateTime.Now;
        }

        public void Delete()
        {
            this.IsDeleted = true;
            this.DeletionTime = DateTime.Now;
            this.LastModificationTime = DateTime.Now;
        }
    }
}