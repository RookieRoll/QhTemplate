using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QhTemplate.ApplicationCore.Areas;
using QhTemplate.ApplicationCore.Authentications.Permissions;
using QhTemplate.ApplicationCore.Authentications.Roles;
using QhTemplate.ApplicationCore.BriefingContents;
using QhTemplate.ApplicationCore.Companys;
using QhTemplate.ApplicationCore.Majors;
using QhTemplate.ApplicationCore.News;
using QhTemplate.ApplicationCore.Organizations;
using QhTemplate.ApplicationCore.RecruitMents;
using QhTemplate.ApplicationCore.Resumes;
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
using QhTemplate.ApplicationService.Resumes;
using QhTemplate.ApplicationService.Schools;
using QhTemplate.ApplicationService.Users;
using QhTemplate.FontWeb.Filer;
using QhTemplate.MysqlEntityFrameWorkCore.Models;

namespace QhTemplate.FontWeb
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(opt =>
            {
                opt.Filters.Add<MyExceptionFilter>();
            });
            services.AddDbContext<EmsDBContext>();
            services.AddScoped<RoleManager>();
            services.AddScoped<UserManager>();
            services.AddScoped<OrganizationManager>();
            services.AddScoped<PermissionManager>();
            services.AddScoped<MajorManager>();
            services.AddScoped<ArticleManagers>();
            services.AddScoped<AreaManager>();
            services.AddScoped<SchoolManagers>();
            services.AddScoped<CompanyManager>();
            services.AddScoped<RecruitmentManager>();
            services.AddScoped<BriefingContentManager>();
            services.AddScoped<ResumeManager>();
            
            services.AddScoped<IUserAppService, UserAppService>();
            services.AddScoped<IRolesAppService, RoleAppService>();
            services.AddScoped<IOrganizationAppService, OrganizationAppService>();
            services.AddScoped<IMajorAppService, MajorAppService>();
            services.AddScoped<IArticlesApplicationService, ArticlesApplicationService>();
            services.AddScoped<IAreaAppService, AreaAppService>();
            services.AddScoped<ISchoolService, SchoolService>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<IRecruitmentServcie, RecruitmentServcie>();
            services.AddScoped<IBriefingContentService, BriefingContentService>();
            services.AddScoped<IResumeService, ResumeService>();
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(options =>
            {
                options.LoginPath = "/Account/Signin";
                options.LogoutPath = "/Account/SignOut";
            });


            services.AddTimedJob();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseTimedJob();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}/{ids?}");
            });
        }
    }
}