using Microsoft.Extensions.DependencyInjection;
using QhTemplate.ApplicationCore.Authentications.Permissions;
using QhTemplate.ApplicationCore.Authentications.Roles;
using QhTemplate.ApplicationCore.Organizations;
using QhTemplate.ApplicationCore.Users;
using QhTemplate.ApplicationService.Authentications;
using QhTemplate.ApplicationService.Organizations;
using QhTemplate.ApplicationService.Users;
using QhTemplate.MysqlEntityFrameWorkCore.Models;

namespace QhTemplate.AdminWeb.MIddleWare
{
    public static class DependencyInjectionExtension
    {
        public static void AddDependency(this IServiceCollection service)
        {
            service.AddScoped<EmsDBContext>();
            service.AddScoped<RoleManager>();
            service.AddScoped<UserManager>();
            service.AddScoped<OrganizationManager>();
            service.AddScoped<PermissionManager>();

            service.AddScoped<IUserAppService, UserAppService>();
            service.AddScoped<IRolesAppService, RoleAppService>();
            service.AddScoped<IOrganizationAppService, OrganizationAppService>();

        }
    }
}