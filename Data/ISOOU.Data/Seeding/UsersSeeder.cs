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
            var roleManager = serviceProvider.GetRequiredService<RoleManager<SystemRole>>();

            var userToSeed = await userManager.FindByNameAsync(GlobalConstants.AdministratorUsername);
            var roleToSeed = (await roleManager.FindByNameAsync(GlobalConstants.AdministratorRoleName)).ToString();
            if (userToSeed == null)
            {
                var userAdmin = new SystemUser()
                {
                    UserName = GlobalConstants.AdministratorUsername,
                    Email = GlobalConstants.AdministratorEmail,
                    FullName = GlobalConstants.AdministratorFullName,
                };

                var result = await userManager.CreateAsync(userAdmin, roleToSeed);
                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }

                await userManager.AddToRoleAsync(userAdmin, GlobalConstants.AdministratorRoleName);
          
            }

            userToSeed = await userManager.FindByEmailAsync("irrrka@imail.com");
            roleToSeed = (await roleManager.FindByNameAsync(GlobalConstants.UserRoleName)).ToString();
            if (userToSeed == null)
            {
                var userIrrrka = new SystemUser()
                {
                    UserName = "Irrrka",
                    Email = "irrrka@imail.com",
                    FullName = "Irina Marina",
                };

                var result = await userManager.CreateAsync(userIrrrka, "123123");
                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }

                await userManager.AddToRoleAsync(userIrrrka, roleToSeed);
            }

            userToSeed = await userManager.FindByEmailAsync("irrrkaDir@imail.com");
            roleToSeed = (await roleManager.FindByNameAsync(GlobalConstants.DirectorRoleName)).ToString();
            if (userToSeed == null)
            {
                var userIrrrkaDir = new SystemUser
                {
                    UserName = "IrrrkaDir",
                    Email = "irrrkaDir@imail.com",
                    FullName = "Irina Dirina",
                };

                var result = await userManager.CreateAsync(userIrrrkaDir, "123123");
                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }

                await userManager.AddToRoleAsync(userIrrrkaDir, roleToSeed);
            }

            userToSeed = await userManager.FindByEmailAsync("jovo@imail.com");
            roleToSeed = (await roleManager.FindByNameAsync(GlobalConstants.UserRoleName)).ToString();
            if (userToSeed == null)
            {
                var userJovo = new SystemUser
                {
                    UserName = "Jovo",
                    Email = "jovo@imail.com",
                    FullName = "Jovo Jovo",
                };

                var result = await userManager.CreateAsync(userJovo, "123123");
                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }

                await userManager.AddToRoleAsync(userJovo, roleToSeed);
            }
        }
    }
}
