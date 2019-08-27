namespace ISOOU.Services.Data.Tests
{
    using System;
    using System.Reflection;
    using AutoMapper.Configuration;
    using CloudinaryDotNet;
    using ISOOU.Data;
    using ISOOU.Data.Common.Repositories;
    using ISOOU.Data.Models;
    using ISOOU.Data.Repositories;
    using ISOOU.Services.Data.Contracts;
    using ISOOU.Services.Mapping;
    using ISOOU.Services.Models;
    using ISOOU.Web.ViewModels.Districts;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

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
            services.AddScoped<ISchoolsService, SchoolsService>();
            services.AddScoped<IAddressesService, AddressesService>();
            services.AddScoped<ICandidatesService, CandidatesService>();
            services.AddScoped<IParentsService, ParentsService>();
            services.AddScoped<ISearchService, SearchService>();
            services.AddScoped<IQuestionsService, QuestionsService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<ICalculatorService, CalculatorService>();
            services.AddScoped<ICloudinaryService, CloudinaryService>();
            services.AddScoped<ICriteriasService, CriteriasService>();

            // Identity stores
            //services.AddTransient<IUserStore<SystemUser>, ApplicationUserStore>();
            //services.AddTransient<IRoleStore<ApplicationRole>, ApplicationRoleStore>();

            // AutoMapper
            AutoMapperConfig.RegisterMappings(
                typeof(DistrictViewModel).GetTypeInfo().Assembly,
                typeof(DistrictServiceModel).GetTypeInfo().Assembly,
                typeof(District).GetTypeInfo().Assembly);

            var context = new DefaultHttpContext();
            services.AddSingleton<IHttpContextAccessor>(new HttpContextAccessor { HttpContext = context });

            return services;
        }

        public void Dispose()
        {
            this.DbContext.Database.EnsureDeleted();
            this.SetServices();
        }
    }
}