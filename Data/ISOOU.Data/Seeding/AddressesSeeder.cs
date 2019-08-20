namespace ISOOU.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using ISOOU.Data.Models;
    using ISOOU.Data.Models.Enums;

    internal class AddressesSeeder : ISeeder
    {
        public async Task SeedAsync(ISOOUDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (!dbContext.Addresses.Any())
            {
                await dbContext.Addresses.AddAsync(new AddressDetails
                {
                    Permanent = "NA",
                    Current = "NA",
                    PermanentCity = CityName.София,
                    CurrentCity = CityName.София,
                    PermanentDistrictId = dbContext.Districts.Where(d => d.Name == "Неизвестен").FirstOrDefault().Id,
                    CurrentDistrictId = dbContext.Districts.Where(d => d.Name == "Неизвестен").FirstOrDefault().Id,
                });
            }
        }
    }
}