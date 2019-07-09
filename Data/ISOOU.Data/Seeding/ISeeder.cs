namespace ISOOU.Data.Seeding
{
    using System;
    using System.Threading.Tasks;

    public interface ISeeder
    {
        Task SeedAsync(ISOOUContext dbContext, IServiceProvider serviceProvider);
    }
}
