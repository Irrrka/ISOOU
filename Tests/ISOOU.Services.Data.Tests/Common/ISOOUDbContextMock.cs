namespace ISOOU.Services.Data.Tests.Common
{
    using System;

    using ISOOU.Data;
    using Microsoft.EntityFrameworkCore;

    public static class ISOOUDbContextFactory
    {
        public static ISOOUDbContext InitializeContext()
        {
            var options = new DbContextOptionsBuilder<ISOOUDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

                return new ISOOUDbContext(options);
        }
    }
}
