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
        
        
    }
}