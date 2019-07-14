namespace ISOOU.Web
{
    using System.Reflection;

    using ISOOU.Data;
    using ISOOU.Data.Common;
    using ISOOU.Data.Common.Repositories;
    using ISOOU.Data.Models;
    using ISOOU.Data.Repositories;
    using ISOOU.Data.Seeding;
    using ISOOU.Services.Data;
    using ISOOU.Services.Mapping;
    using ISOOU.Services.Messaging;
    using ISOOU.Web.Areas.Identity.Pages.Account.Manage;
    using ISOOU.Web.ViewModels;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Framework services
            // TODO: Add pooling when this bug is fixed: https://github.com/aspnet/EntityFrameworkCore/issues/9741
            services.AddDbContext<ISOOUDbContext>(
                options => options.UseSqlServer(this.configuration.GetConnectionString("DefaultConnection")));

            services
                .AddIdentity<SystemUser, ApplicationRole>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequiredLength = 6;
                })
                .AddEntityFrameworkStores<ISOOUDbContext>()
                .AddUserStore<ISOOUUserStore>()
                .AddRoleStore<ISOOURoleStore>()
                .AddDefaultTokenProviders()
                .AddDefaultUI(UIFramework.Bootstrap4);

            services
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddRazorPagesOptions(options =>
                {
                    options.AllowAreas = true;
                    options.Conventions.AuthorizeAreaFolder("Identity", "/Account/Manage");
                    options.Conventions.AuthorizeAreaPage("Identity", "/Account/Logout");
                });

            services
                .ConfigureApplicationCookie(options =>
                {
                    options.LoginPath = "/Identity/Account/Login";
                    options.LogoutPath = "/Identity/Account/Logout";
                    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                });

            services
                .Configure<CookiePolicyOptions>(options =>
                {
                    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                    options.CheckConsentNeeded = context => true;
                    options.MinimumSameSitePolicy = SameSiteMode.Lax;
                    options.ConsentCookie.Name = ".AspNetCore.ConsentCookie";
                });

            

            services.AddSingleton(this.configuration);

            // Identity stores
            services.AddTransient<IUserStore<SystemUser>, ISOOUUserStore>();
            services.AddTransient<IRoleStore<ApplicationRole>, ISOOURoleStore>();

            // Data repositories
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IDbQueryRunner, DbQueryRunner>();

            // Application services
            services.AddTransient<IEmailSender, NullMessageSender>();
            services.AddTransient<ISmsSender, NullMessageSender>();
            services.AddTransient<ISettingsService, SettingsService>();

            // Entity services
            services.AddTransient<ISchoolsService, SchoolsService>();
            services.AddTransient<IDistrictsService, DistrictsService>();
            services.AddSingleton<AllDistrictsViewModel>();
            services.AddSingleton<SchoolViewModel>();
            services.AddTransient<FilterSchoolsViewModel>();
            services.AddTransient<FilterCandidateInputModel>();
            services.AddTransient<StatusCandidateViewModel>();
            services.AddTransient<StatusCandidatesViewModel>();
            services.AddTransient<ParentModel>();
            services.AddTransient<ChildModel>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            // Seed data on application startup
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ISOOUDbContext>();

                if (env.IsDevelopment())
                {
                    dbContext.Database.Migrate();
                }

                new ISOOUContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute("areaRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });

            //var dataText = System.IO.File.ReadAllText(@"schoolsseeddata.txt");

            //private async Task CreateRoles(IServiceProvider serviceProvider)
            //{
            //    //initializing custom roles 
            //    var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            //    var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            //    string[] roleNames = { "Admin", "Store-Manager", "Member" };
            //    IdentityResult roleResult;

            //    foreach (var roleName in roleNames)
            //    {
            //        var roleExist = await RoleManager.RoleExistsAsync(roleName);
            //        // ensure that the role does not exist
            //        if (!roleExist)
            //        {
            //            //create the roles and seed them to the database: 
            //            roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
            //        }
            //    }

            //// find the user with the admin email 
            //var _user = await UserManager.FindByEmailAsync("admin@email.com");

            //// check if the user exists
            //if (_user == null)
            //{
            //    //Here you could create the super admin who will maintain the web app
            //    var poweruser = new ApplicationUser
            //    {
            //        UserName = "Admin",
            //        Email = "admin@email.com",
            //    };
            //    string adminPassword = "p@$$w0rd";

            //    var createPowerUser = await UserManager.CreateAsync(poweruser, adminPassword);
            //    if (createPowerUser.Succeeded)
            //    {
            //        //here we tie the new user to the role
            //        await UserManager.AddToRoleAsync(poweruser, "Admin");

            //    }
            //}
            //}
        }
    }
}
