namespace ISOOU.Services.Data.Tests
{
    using System;
    using System.Reflection;
    using ISOOU.Data;
    using ISOOU.Data.Common.Repositories;
    using ISOOU.Data.Models;
    using ISOOU.Data.Repositories;
    using ISOOU.Services.Data.Contracts;
    using ISOOU.Services.Mapping;
    using ISOOU.Services.Messaging;
    using ISOOU.Web.ViewModels;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    

    public abstract class BaseServiceTests : IDisposable
    {
        protected BaseServiceTests()
        {
            var services = this.SetServices();

            this.ServiceProvider = services.BuildServiceProvider();
            this.DbContext = this.ServiceProvider.GetRequiredService<ISOOUDbContext>();
        }

        protected IServiceProvider ServiceProvider { get; set; }

        protected ISOOUDbContext DbContext { get; set; }

        private ServiceCollection SetServices()
        {
            var services = new ServiceCollection();

            services.AddDbContext<ISOOUDbContext>(
                opt => opt.UseInMemoryDatabase(Guid.NewGuid().ToString()));

            services
                    .AddIdentity<SystemUser, SystemRole>(options =>
                    {
                        options.Password.RequireDigit = false;
                        options.Password.RequireLowercase = false;
                        options.Password.RequireUppercase = false;
                        options.Password.RequireNonAlphanumeric = false;
                        options.Password.RequiredLength = 6;
                    })
                    .AddEntityFrameworkStores<ISOOUDbContext>()
                    //.AddUserStore<ApplicationUserStore>()
                   // .AddRoleStore<ApplicationRoleStore>()
                    .AddDefaultTokenProviders();

            // Data repositories
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

           
            services.AddScoped<IDistrictsService, DistrictsService>();
            
            // Identity stores
            //services.AddTransient<IUserStore<SystemUser>, ApplicationUserStore>();
            //services.AddTransient<IRoleStore<ApplicationRole>, ApplicationRoleStore>();

            // AutoMapper
            AutoMapperConfig.RegisterMappings(typeof(DistrictViewModel).GetTypeInfo().Assembly);

           

            var context = new DefaultHttpContext();
            services.AddSingleton<IHttpContextAccessor>(new HttpContextAccessor { HttpContext = context });

            return services;
        }

        public void Dispose()
        {
            DbContext.Database.EnsureDeleted();
            this.SetServices();
        }
    }
}