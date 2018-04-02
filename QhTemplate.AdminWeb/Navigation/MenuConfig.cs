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
            #region 基础菜单
            MenuItem userMenu = new MenuItem() { Name = "基础", RequiredAuthorizeCode = PermissionNames.BaseMenu };
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
            #endregion
            var major = new MenuItem { Name = "专业", Url = "/Major/Index", RequiredAuthorizeCode = null };
            SideMenu.Children.Add(major);

            var article = new MenuItem { Name = "新闻", Url = "/Articles/Index", RequiredAuthorizeCode = null };
            SideMenu.Children.Add(article);

            var area = new MenuItem { Name = "区域学校", Url = "/Area/Index", RequiredAuthorizeCode = null };
            SideMenu.Children.Add(area);
        }
    }
}