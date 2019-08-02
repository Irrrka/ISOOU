namespace ISOOU.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using ISOOU.Data.Models;
    using ISOOU.Data.Models.Enums;

    internal class DistrictsSeeder : ISeeder
    {
        public async Task SeedAsync(ISOOUDbContext dbContext, IServiceProvider serviceProvider)
        {

            if (!dbContext.Districts.Any())
            {
             await dbContext.Districts.AddAsync(new District { Name = "Връбница" });
             await dbContext.Districts.AddAsync(new District { Name = "Възраждане" });
             await dbContext.Districts.AddAsync(new District { Name = "Изгрев" });
             await dbContext.Districts.AddAsync(new District { Name = "Илинден" });
             await dbContext.Districts.AddAsync(new District { Name = "Красна Поляна" });
             await dbContext.Districts.AddAsync(new District { Name = "Красно Село" });
             await dbContext.Districts.AddAsync(new District { Name = "Лозенец" });
             await dbContext.Districts.AddAsync(new District { Name = "Люлин" });
             await dbContext.Districts.AddAsync(new District { Name = "Младост" });
             await dbContext.Districts.AddAsync(new District { Name = "Надежда" });
             await dbContext.Districts.AddAsync(new District { Name = "Оборище" });
             await dbContext.Districts.AddAsync(new District { Name = "Овча Купел" });
             await dbContext.Districts.AddAsync(new District { Name = "Подуяне" });
             await dbContext.Districts.AddAsync(new District { Name = "Сердика" });
             await dbContext.Districts.AddAsync(new District { Name = "Средец" });
             await dbContext.Districts.AddAsync(new District { Name = "Триадица" });
             await dbContext.Districts.AddAsync(new District { Name = "Искър" });
            }
        }
    }
}