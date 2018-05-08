using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using QhTemplate.ApplicationCore.Authentications.Permissions;
using QhTemplate.ApplicationCore.Authentications.Roles;
using QhTemplate.ApplicationCore.Users;
using QhTemplate.MysqlEntityFrameWorkCore.Models;

namespace QhTemplate.ApplicationService.Users
{
    public class UserAppService : IUserAppService
    {
        private readonly UserManager _userManager;
        private readonly RoleManager _roleManager;
        private readonly PermissionManager _permissionManager;
        private readonly EmsDBContext _db;

        public UserAppService(UserManager userManager, RoleManager roleManager, PermissionManager permissionManager,
            EmsDBContext db)
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

        public void Create(string userName, string name, string email, UserType type)
        {
            var user = User.Create(name, userName, email, type);
            Func<Role, bool> func = GetDefaultRole(type);
            var defaultRole = _roleManager.Finds(func).Select(m => m.Id);

            using (var scope = _db.Database.BeginTransaction())
            {
                try
                {
                    _userManager.Create(user);
                    _roleManager.SetUserRole(user.Id, defaultRole.ToArray());
                    scope.Commit();
                }
                catch (Exception ex)
                {
                    scope.Rollback();
                }
            }
        }

        private Func<Role, bool> GetDefaultRole(UserType type)
        {
            Func<Role, bool> func;
            switch (type)
            {
                case UserType.Employee: func = m => m.Id == 3 && m.IsDefault; break;
                case UserType.Teacher: func = m => m.IsDefault && m.Id == 2; break;
                case UserType.Admin: func = m => m.IsDefault && m.Id == 1; break;
                default: func = m => m.IsDefault && !m.IsStatic; break;
            }
            return func;
        }
        public int Register(string userName, string name, string email, string password, UserType type)
        {
            var user = User.Register(name, userName, email, password, type);
            var defaultRole = _roleManager.Finds(m => m.IsDefault).Select(m => m.Id);

            using (var scope = _db.Database.BeginTransaction())
            {
                try
                {
                    _userManager.Create(user);
                    _roleManager.SetUserRole(user.Id, defaultRole.ToArray());
                    scope.Commit();
                }
                catch (Exception ex)
                {
                    scope.Rollback();
                }
            }

            return user.Id;
        }
        public void Remove(int? id)
        {
            _userManager.Deleted(id);
        }

        public void Update(User user)
        {
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
            using (var scope = _db.Database.BeginTransaction())
            {
                _roleManager.SetUserRole(id, roles);
                scope.Commit();
            }
        }

        public void SetAuthorize(int id, string[] permissions)
        {
            _permissionManager.SetUserPermissions(id, permissions);
        }

        public IEnumerable<User> GetUsersByCompany(int companyId)
        {
            var users = (from cu in _db.CompanyUser
                         join us in Finds() on cu.UserId equals us.Id
                         where cu.CompanyId == companyId
                         select us);
            return users;
        }
    }
}