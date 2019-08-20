namespace ISOOU.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using ISOOU.Data.Models;
    using ISOOU.Data.Models.Enums;

    internal class ParentsSeeder : ISeeder
    {
        public async Task SeedAsync(ISOOUDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (!dbContext.Parents.Any())
            {
                var address = dbContext.Addresses.FirstOrDefault();
                await dbContext.Parents.AddAsync(new Parent
                {
                    FirstName = "Друг",
                    MiddleName = "NA",
                    LastName = " ",
                    PhoneNumber = "08888888888",
                    Role = ParentRole.Друг,
                    AddressId = address.Id,
                    UCN = "0000000001",
                    WorkDistrict = dbContext.Districts.FirstOrDefault(n => n.Name == "Неизвестен"),
                });
                await dbContext.Parents.AddAsync(new Parent
                {
                    FirstName = "Няма",
                    MiddleName = "NA",
                    LastName = " ",
                    PhoneNumber = "08888888888",
                    Role = ParentRole.Няма,
                    AddressId = address.Id,
                    UCN = "0000000002",
                    WorkDistrict = dbContext.Districts.FirstOrDefault(n => n.Name == "Неизвестен"),
                });
            }
        }
    }
}