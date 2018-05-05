using Microsoft.Extensions.DependencyInjection;
using QhTemplate.AdminWeb.Navigation;
using QhTemplate.ApplicationCore.Areas;
using QhTemplate.ApplicationCore.Authentications.Permissions;
using QhTemplate.ApplicationCore.Authentications.Roles;
using QhTemplate.ApplicationCore.BriefingContents;
using QhTemplate.ApplicationCore.Companys;
using QhTemplate.ApplicationCore.Majors;
using QhTemplate.ApplicationCore.News;
using QhTemplate.ApplicationCore.Organizations;
using QhTemplate.ApplicationCore.RecruitMents;
using QhTemplate.ApplicationCore.Schools;
using QhTemplate.ApplicationCore.Users;
using QhTemplate.ApplicationService.Areas;
using QhTemplate.ApplicationService.Authentications;
using QhTemplate.ApplicationService.BriefingContents;
using QhTemplate.ApplicationService.Companys;
using QhTemplate.ApplicationService.Majors;
using QhTemplate.ApplicationService.NewsArticles;
using QhTemplate.ApplicationService.Organizations;
using QhTemplate.ApplicationService.Recruitments;
using QhTemplate.ApplicationService.Schools;
using QhTemplate.ApplicationService.Users;
using QhTemplate.MysqlEntityFrameWorkCore.Models;

namespace QhTemplate.AdminWeb.MIddleWare
{
    public static class DependencyInjectionExtension
    {
        public static void AddDependency(this IServiceCollection service)
        {
            service.AddDbContext<EmsDBContext>();
            service.AddScoped<RoleManager>();
            service.AddScoped<UserManager>();
            service.AddScoped<OrganizationManager>();
            service.AddScoped<PermissionManager>();
            service.AddScoped<MenuProvider>();
            service.AddScoped<MajorManager>();
            service.AddScoped<ArticleManagers>();
            service.AddScoped<AreaManager>();
            service.AddScoped<SchoolManagers>();
            service.AddScoped<CompanyManager>();
            service.AddScoped<RecruitmentManager>();
            service.AddScoped<BriefingContentManager>();
            
            service.AddScoped<IUserAppService, UserAppService>();
            service.AddScoped<IRolesAppService, RoleAppService>();
            service.AddScoped<IOrganizationAppService, OrganizationAppService>();
            service.AddScoped<IMajorAppService, MajorAppService>();
            service.AddScoped<IArticlesApplicationService, ArticlesApplicationService>();
            service.AddScoped<IAreaAppService, AreaAppService>();
            service.AddScoped<ISchoolService, SchoolService>();
            service.AddScoped<ICompanyService, CompanyService>();
            service.AddScoped<IRecruitmentServcie, RecruitmentServcie>();
            service.AddScoped<IBriefingContentService, BriefingContentService>();
        }
    }
}