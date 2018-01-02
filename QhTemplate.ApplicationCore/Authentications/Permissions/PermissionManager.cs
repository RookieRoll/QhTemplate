using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;
using QhTemplate.ApplicationCore.Exceptions;
using QhTemplate.MysqlEntityFrameWorkCore.Models;

namespace QhTemplate.ApplicationCore.Authentications.Permissions
{
    public class PermissionManager:BaseManager<Permission>
    {
        public PermissionManager(EmsDBContext db) : base(db)
        {
        }

        public List<string> GetUserPermissions(int? userId)
        {
            var user = _db.User.Include(m => m.UserRole).SingleOrDefault(m => m.Id == userId);
            
            if(user==null)
                throw new UserFriendlyException("该用户不存在");

            var roleIds = user.UserRole.Select(m => m.RoleId);
            var permissionCodes = Finds().Where(u => u.UserId == user.Id || roleIds.Any(x => x == u.RoleId))
                .Select(d => d.Code).Distinct().ToList();
            return permissionCodes;
        }
        
        public void SetUserPermissions(int userId, string[] permissionCodes)
        {
            var originPermission =Finds(m => m.UserId == userId).ToList();
            _db.Permission.RemoveRange(originPermission);
            
            if (permissionCodes != null)
            {
                var permissions = permissionCodes.Select(code => new Permission
                {
                    UserId = userId,
                    Code = code,
                    CreationTime = DateTime.Now,
                }).ToList();
                _db.Permission.AddRange(permissions);
            }
            Save();
        }
        
        public List<string> GetPermissionsByUser(int userId)
        {
            var user = _db.User.SingleOrDefault(d => d.Id == userId);
            if (user == null)
                throw  new UserFriendlyException("该用户不存在");
            var permissionCodes =Finds(d => d.UserId == user.Id)
                .Select(d => d.Code).Distinct()
                .ToList();
            return permissionCodes;
        }
        
        public IEnumerable<string> GetRolePermissions(int? roleId = null)
        {
            return Finds(m => m.RoleId == roleId && m.UserId == null)
                .Select(m => m.Code)
                .ToList();
        }
        
        public bool IsGranted(int userId, string permission)
        {
            var list = GetUserPermissions(userId);
            return list.Contains(permission);
        }
    }
}