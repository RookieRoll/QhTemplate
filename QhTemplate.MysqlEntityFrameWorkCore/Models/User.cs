using System;
using System.Collections.Generic;

namespace QhTemplate.MysqlEntityFrameWorkCore.Models
{
    public partial class User
    {
        public User()
        {
            CompanyUser = new HashSet<CompanyUser>();
            UserOrganization = new HashSet<UserOrganization>();
            UserRole = new HashSet<UserRole>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string EmailAddress { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public DateTime? DeletionTime { get; set; }
        public int UserType { get; set; }

        public ICollection<CompanyUser> CompanyUser { get; set; }
        public ICollection<UserOrganization> UserOrganization { get; set; }
        public ICollection<UserRole> UserRole { get; set; }

        public static User Create(string name, string username, string email, UserType type)
        {
            return new User()
            {
                Name = name,
                UserName = username,
                Password = "123456",
                EmailAddress = email,
                IsDeleted = false,
                CreationTime = DateTime.Now,
                LastModificationTime = DateTime.Now,
                UserType = (int) type
            };
        }
        
        public static User Register(string name, string username, string email, string password,UserType type)
        {
            return new User()
            {
                
                Name = name,
                UserName = username,
                Password = password,
                EmailAddress = email,
                IsDeleted = false,
                CreationTime = DateTime.Now,
                LastModificationTime = DateTime.Now,
                UserType = (int) type
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