namespace ISOOU.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using ISOOU.Common;
    using ISOOU.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    public class UsersSeeder : ISeeder
    {
        public async Task SeedAsync(ISOOUDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<SystemUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<SystemUser>>();

            var admin = userManager.FindByNameAsync(GlobalConstants.AdministratorRoleName);
            Task<SystemUser> role = roleManager.FindByNameAsync(GlobalConstants.AdministratorRoleName);
            if (admin == null)
            {
                var userAdmin = new SystemUser()
                {
                    UserName = GlobalConstants.AdministratorUsername,
                    Email = GlobalConstants.AdministratorEmail,
                    FullName = GlobalConstants.AdministratorFullName,
                };

                var result = await userManager.CreateAsync(userAdmin, GlobalConstants.AdministratorPassword);
                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }

                await userManager.AddToRoleAsync(userAdmin, GlobalConstants.AdministratorRoleName);
                await dbContext.Users.AddAsync(userAdmin);
            }

            if (userManager.FindByEmailAsync("irrrka@imail.com") == null)
            {
                var userIrrrka = new SystemUser()
                {
                    UserName = "Irrrka",
                    Email = "irrrka@imail.com",
                    FullName = "Irina Marina",
                };

                var result = await userManager.CreateAsync(userIrrrka, "123123");
                //if (!result.Succeeded)
                //{
                //    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                //}

                await userManager.AddToRoleAsync(userIrrrka, GlobalConstants.UserRoleName);
                await dbContext.Users.AddAsync(userIrrrka);
            }

            if (userManager.FindByEmailAsync("irrrkaDir@imail.com") == null)
            {
                var userIrrrkaDir = new SystemUser
                {
                    UserName = "IrrrkaDir",
                    Email = "irrrkaDir@imail.com",
                    FullName = "Irina Dirina",
                };

                var result = await userManager.CreateAsync(userIrrrkaDir, "123123");
                //if (!result.Succeeded)
                //{
                //    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                //}

                await userManager.AddToRoleAsync(userIrrrkaDir, GlobalConstants.DirectorRoleName);
                await dbContext.Users.AddAsync(userIrrrkaDir);
            }

            if (userManager.FindByEmailAsync("jovo@imail.com") == null)
            {
                var userJovo = new SystemUser
                {
                    UserName = "Jovo",
                    Email = "jovo@imail.com",
                    FullName = "Jovo Jovo",
                };

                var result = await userManager.CreateAsync(userJovo, "123123");
                //if (!result.Succeeded)
                //{
                //    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                //}

                await userManager.AddToRoleAsync(userJovo, GlobalConstants.UserRoleName);
                await dbContext.Users.AddAsync(userJovo);
            }
        }
    }
}
