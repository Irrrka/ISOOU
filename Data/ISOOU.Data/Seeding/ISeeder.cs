namespace ISOOU.Data.Seeding
{
    using System;
    using System.Threading.Tasks;

    public interface ISeeder
    {
        Task SeedAsync(ISOOUDbContext dbContext, IServiceProvider serviceProvider);
    }
}
