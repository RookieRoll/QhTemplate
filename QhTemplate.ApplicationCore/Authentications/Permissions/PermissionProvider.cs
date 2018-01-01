using System.Collections.Generic;

namespace QhTemplate.ApplicationCore.Authentications.Permissions
{
    public class PermissionProvider
    {
        public static readonly List<Permissions> PermissionNodes = new List<Permissions> {
            new Permissions { Name=PermissionNames.Total,DisplayName="所有"},
            new Permissions { Name = PermissionNames.User, DisplayName = "用户" },
            new Permissions { Name =PermissionNames.Role, DisplayName = "角色" },
            #region 组织结构
            new Permissions { Name = PermissionNames.Organization, DisplayName = "组织" },
            new Permissions { Name = PermissionNames.Organization_Create,DisplayName = "创建组织" },
            new Permissions { Name = PermissionNames.Organization_Delete,DisplayName = "删除组织" },
            new Permissions { Name = PermissionNames.Organization_Rename,DisplayName = "重命名组织" },
            new Permissions { Name = PermissionNames.Organization_Migrate,DisplayName = "迁移组织" },
            new Permissions { Name = PermissionNames.Organization_AddUser,DisplayName = "添加组织成员" },
            new Permissions { Name = PermissionNames.Organization_RemoveUser,DisplayName = "移除组织成员" },
            #endregion
        };
        public class Permissions
        {
            public string Name { get; set; }
            public string DisplayName { get; set; }
        }
    }
}