using System.Collections.Generic;
using QhTemplate.ApplicationCore.Authentications.Permissions;

namespace QhTemplate.AdminWeb.Navigation
{
    public class MenuConfig
    {
        public static MenuItem SideMenu { get; set; } = new MenuItem();

        static MenuConfig()
        {
            SideMenu = new MenuItem() { Children = new List<MenuItem>() };
            MenuItem userMenu = new MenuItem() { Name = "基础", RequiredAuthorizeCode = PermissionNames.User };
            //创建用户相关的权限
            MenuItem item = new MenuItem { Name = "用户", Url = "/User/Index", RequiredAuthorizeCode = PermissionNames.User };
            userMenu.Children.Add(item);
            //创建权限相关的菜单
            item = new MenuItem { Name = "角色", Url = "/Role/Index", RequiredAuthorizeCode = PermissionNames.Role };
            userMenu.Children.Add(item);
            //创建组织相关的菜单
            item = new MenuItem { Name = "组织结构", Url = "/Organization/Index", RequiredAuthorizeCode = PermissionNames.Organization };
            userMenu.Children.Add(item);
            SideMenu.Children.Add(userMenu);
        }
    }
}