namespace ISOOU.Data
{
    using System;
    using System.Threading.Tasks;

    using ISOOU.Data.Common;

    using Microsoft.EntityFrameworkCore;

    public class DbQueryRunner : IDbQueryRunner
    {
        public DbQueryRunner(ISOOUContext context)
        {
            this.Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public ISOOUContext Context { get; set; }

        public Task RunQueryAsync(string query, params object[] parameters)
        {
            return this.Context.Database.ExecuteSqlCommandAsync(query, parameters);
        }

        public void Dispose()
        {
            this.Context?.Dispose();
        }
    }
}
