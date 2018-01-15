using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Caching.Memory;
using QhTemplate.ApplicationCore.Authentications.Permissions;

namespace QhTemplate.AdminWeb.Navigation
{
    public class MenuProvider
    {
        private readonly PermissionManager _permission;
        
        private readonly IMemoryCache _cache;
        public MenuProvider(PermissionManager permission, IMemoryCache cache)
        {
            _permission = permission;
            this._cache = cache;
        }

        public MenuItem GetMenuByUserId(int userId)
        {
            if (_cache.TryGetValue(userId, out MenuItem result))
                return result;
            result = LoadMenu(userId);

            return result;
        }

        public void ReloadMenu(int userId)
        {
            if (RemoveMenu(userId))
                LoadMenu(userId);
        }
        public MenuItem LoadMenu(int userId)
        {
            var menu = MenuConfig.SideMenu;
            var list = _permission.GetUserPermissions(userId);
            var result = GetFilterMenu(menu, list);
            _cache.Set(userId,result);
            return result;
        }

        public bool RemoveMenu(int userId)
        {
            if (!_cache.TryGetValue(userId, out MenuItem items)) return false;
            _cache.Remove(userId);
            return true;

        }

        private MenuItem GetFilterMenu(MenuItem source, ICollection<string> permission)
        {
            if (!string.IsNullOrWhiteSpace(source.RequiredAuthorizeCode) &&
                !permission.Contains(source.RequiredAuthorizeCode)) return null;
            var node = new MenuItem() { Name = source.Name, RequiredAuthorizeCode = source.RequiredAuthorizeCode, Url = source.Url };
            var list = new List<MenuItem>();
            if (source.HasChild)
            {
                source.Children.ForEach(d =>
                {
                    var child = GetFilterMenu(d, permission);
                    if (child != null)
                    {
                        list.Add(child);
                    }
                });
            }
            node.Children = list;
            return node;
        }
        
        private MenuItem GetFilterMenu(int userId, MenuItem source)
        {
            var node = new MenuItem() { Name = source.Name, RequiredAuthorizeCode = source.RequiredAuthorizeCode, Url = source.Url };
            var list = new List<MenuItem>();
            if (source.HasChild)
            {
                list.AddRange(
                    from child in source.Children
                    where string.IsNullOrWhiteSpace(child.RequiredAuthorizeCode)
                          || _permission.IsGranted(userId, child.RequiredAuthorizeCode)
                    select GetFilterMenu(userId, child));
            }
            node.Children = list;
            return node;
        }
    }
}