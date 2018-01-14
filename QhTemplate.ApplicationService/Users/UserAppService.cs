using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QhTemplate.ApplicationCore.Authentications.Permissions;
using QhTemplate.ApplicationCore.Authentications.Roles;
using QhTemplate.ApplicationCore.Users;
using QhTemplate.MysqlEntityFrameWorkCore.Models;

namespace QhTemplate.ApplicationService.Users
{
    public class UserAppService:IUserAppService
    {
        private readonly UserManager _userManager;
        private readonly RoleManager _roleManager;
        private readonly PermissionManager _permissionManager;
        private readonly EmsDBContext _db;

        public UserAppService(UserManager userManager, RoleManager roleManager, PermissionManager permissionManager, EmsDBContext db)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _permissionManager = permissionManager;
            _db = db;
        }

        public IQueryable<User> Finds()
        {
            return _userManager.Finds().AsQueryable();
        }

        public IQueryable<User> Finds(Func<User, bool> func)
        {
            return _userManager.Finds(func).AsQueryable();
        }

        public User FirstOrDefault(Func<User, bool> func)
        {
            return _userManager.FirstOrDefault(func);
        }

        public User First(Func<User, bool> func)
        {
            return _userManager.First(func);
        }

        public User Find(int? id)
        {
            return _userManager.Find(id);
        }

        public void Create(string userName, string name, string email)
        {
            var user = User.Create(name, userName, email);
            var defaultRole = _roleManager.Finds(m => m.IsDefault).Select(m => m.Id);
            using (var scope=_db.Database.BeginTransaction())
            {
                _userManager.Create(user);
                _roleManager.SetUserRole(user.Id,defaultRole.ToArray());
                scope.Commit();
            }
        }

        public void Remove(int? id)
        {
            _userManager.Deleted(id);
        }

        public void Update(User user)
        {
            user.Update(user.UserName,user.EmailAddress);
            _userManager.Update(user);
        }

        public IEnumerable<string> GetAuthorize(int id)
        {
            return _permissionManager.GetUserPermissions(id);
        }

        public IEnumerable<string> GetAuthorizeByUserId(int id)
        {
            return _permissionManager.GetPermissionsByUser(id);
        }

        public IEnumerable<Role> GetRolesByUserId(int id)
        {
            return _roleManager.Finds(m => m.UserRole.Any(x => x.UserId == id));
        }

        public IEnumerable<Role> GetRoles()
        {
            return _roleManager.Finds();
        }

        public void SetRoleByUserId(int id, int[] roles)
        {
            _roleManager.SetUserRole(id,roles);
        }

        public void SetAuthorize(int id, string[] permissions)
        {
            _permissionManager.SetUserPermissions(id,permissions);
        }
    }
}
