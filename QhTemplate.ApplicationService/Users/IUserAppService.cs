using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QhTemplate.MysqlEntityFrameWorkCore.Models;

namespace QhTemplate.ApplicationService.Users
{
    public interface IUserAppService
    {
        IQueryable<User> Finds();
        IQueryable<User> Finds(Func<User,bool> func);
        User FirstOrDefault(Func<User, bool> func);
        User First(Func<User, bool> func);
        User Find(int? id);
        void Create(string userName, string name, string email);
        void Remove(int? id);
        void Update(User user);
        IEnumerable<string> GetAuthorize(int id);
        IEnumerable<string> GetAuthorizeByUserId(int id);
        IEnumerable<Role> GetRolesByUserId(int id);
        IEnumerable<Role> GetRoles();
        void SetRoleByUserId(int id, int[] roles);
        void SetAuthorize(int id, string[] permissions);
    }
}
