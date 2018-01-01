using System;
using System.Linq;
using QhTemplate.ApplicationCore.Exceptions;
using QhTemplate.MysqlEntityFrameWorkCore.Models;

namespace QhTemplate.ApplicationCore.Authentications.Roles
{
    public class RoleManager : BaseManager<Role>
    {
        public RoleManager(EmsDBContext db) : base(db)
        {
        }

        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="role"></param>
        /// <param name="permissions"></param>
        /// <exception cref="UserFriendlyException"></exception>
        public void Create(Role role, string[] permissions)
        {
            if (IsRoleNameExit(role.Name))
                throw new UserFriendlyException("该角色名称已存在");
            using (var scope = _db.Database.BeginTransaction())
            {
                _db.Role.Add(role);
                Save();
                SetRolePermissions(role.Id, permissions);
                Save();
                scope.Commit();
            }
        }

        /// <summary>
        /// 给角色设置权限
        /// </summary>
        /// <param name="id"></param>
        /// <param name="permissionNames"></param>
        private void SetRolePermissions(int? id, string[] permissionNames)
        {
            _db.Permission.AddRange(permissionNames.Select(m => new Permission()
            {
                RoleId = id,
                Code = m,
                CreationTime = DateTime.Now
            }));
        }

        /// <summary>
        /// 更新权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="roleName"></param>
        /// <param name="isDefault"></param>
        /// <param name="permissionNames"></param>
        /// <exception cref="UserFriendlyException"></exception>
        public void Update(int? roleId, string roleName, bool isDefault, string[] permissionNames)
        {
            if (IsRoleNameExit(roleName, roleId))
                throw new UserFriendlyException("该角色名称已存在");

            using (var scope = _db.Database.BeginTransaction())
            {
                var role = Find(roleId);
                role.Update(roleName, isDefault);
                DeleteRolePermissions(role.Id);
                SetRolePermissions(role.Id, permissionNames);
                Save();
                scope.Commit();
            }
        }

        public void Delete(int? id)
        {
            var role = Find(id);
            using (var scope = _db.Database.BeginTransaction())
            {
                role.IsDeleted = true;
                role.Delete();
                DeleteRolePermissions(role.Id);
                Save();
                scope.Commit();
            }
        }

        private void DeleteRolePermissions(int? roleId)
        {
            var role = Find(roleId);
            var permissions = _db.Permission.Where(m => m.RoleId == roleId);
            role.UserRole.Clear();
            _db.Permission.RemoveRange(permissions);
        }

        public Role Find(int? id)
        {
            var role = FirstOrDefault(m => m.Id == id);
            return role ?? throw new UserFriendlyException("该角色不存在");
        }

        public void SetUserRole(int userId, int[] roleIds)
        {
            var user = _db.User.FirstOrDefault(m => m.Id == userId);
            var db = _db.UserRole;
            if (user == null)
                throw new UserFriendlyException("该用户不存在");
            using (var scope =_db.Database.BeginTransaction())
            {
                var userRoles = db.Where(m => m.UserId == userId);
                db.RemoveRange(userRoles);
                Save();
                if (roleIds.Length != 0)
                {
                    var roles = Finds(d => roleIds.Contains(d.Id))
                        .Select(m => new UserRole
                        {
                            UserId = userId,
                            RoleId = m.Id
                        });
                    db.AddRange(roles);
                    Save();
                }
                scope.Commit();
            }
           
        }

        /// <summary>
        /// 判断根据角色名判断该角色在数据库中是否存在
        /// </summary>
        /// <param name="roleName"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool IsRoleNameExit(string roleName, int? id = null)
        {
            var role = FirstOrDefault(m => m.Name.Equals(roleName) && m.Id != id);
            return role != null;
        }
    }
}