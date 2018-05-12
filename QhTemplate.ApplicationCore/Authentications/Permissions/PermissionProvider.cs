using System.Collections.Generic;
using QhTemplate.MysqlEntityFrameWorkCore.Models;

namespace QhTemplate.ApplicationCore.Authentications.Permissions
{
    public class PermissionProvider
    {
        public static readonly List<Permissions> PermissionNodes = new List<Permissions>
        {
            new Permissions {Name = PermissionNames.Total, DisplayName = "所有"},
            new Permissions {Name = PermissionNames.BaseMenu, DisplayName = "基础菜单"},

            #region 用户

            new Permissions {Name = PermissionNames.User, DisplayName = "用户"},
            new Permissions {Name = PermissionNames.User_Create, DisplayName = "用户新增"},
            new Permissions {Name = PermissionNames.User_Edit, DisplayName = "用户修改"},
            new Permissions {Name = PermissionNames.User_Delete, DisplayName = "用户删除"},
            new Permissions {Name = PermissionNames.User_Permission, DisplayName = "设置权限"},
            new Permissions {Name = PermissionNames.User_Role, DisplayName = "设置角色"},

            #endregion

            #region 角色

            new Permissions {Name = PermissionNames.Role, DisplayName = "角色"},
            new Permissions {Name = PermissionNames.Role_Create, DisplayName = "角色创建"},
            new Permissions {Name = PermissionNames.Role_Update, DisplayName = "角色更新"},
            new Permissions {Name = PermissionNames.Role_Delete, DisplayName = "角色删除"},

            #endregion


            #region 组织结构

            new Permissions {Name = PermissionNames.Organization, DisplayName = "组织"},
            new Permissions {Name = PermissionNames.Organization_Create, DisplayName = "创建组织"},
            new Permissions {Name = PermissionNames.Organization_Delete, DisplayName = "删除组织"},
            new Permissions {Name = PermissionNames.Organization_Rename, DisplayName = "重命名组织"},
            new Permissions {Name = PermissionNames.Organization_Migrate, DisplayName = "迁移组织"},
            new Permissions {Name = PermissionNames.Organization_AddUser, DisplayName = "添加组织成员"},
            new Permissions {Name = PermissionNames.Organization_RemoveUser, DisplayName = "移除组织成员"},

            #endregion

            new Permissions {Name = PermissionNames.Major, DisplayName = "学科专业管理"},

            new Permissions{Name = PermissionNames.Article,DisplayName = "新闻管理"},
            new Permissions {Name = PermissionNames.SchoolArea, DisplayName = "地区学校管理"},
            new Permissions {Name = PermissionNames.Company, DisplayName = "公司管理"},

            //new Permissions {Name = PermissionNames.RecruitMent, DisplayName = "招聘管理"},
            //new Permissions {Name = PermissionNames.CareerTalk, DisplayName = "宣讲会管理"},

            //new Permissions {Name = PermissionNames.Resument, DisplayName = "简历查看"}
        };

        public class Permissions
        {
            public string Name { get; set; }
            public string DisplayName { get; set; }
        }
    }
}