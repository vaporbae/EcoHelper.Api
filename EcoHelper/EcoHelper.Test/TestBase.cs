namespace EcoHelper.Test
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using EcoHelper.Persistence;

    public class TestBase
    {
        public EcoHelperDbContext GetDbContext(bool useSqlLite = false)
        {
            var builder = new DbContextOptionsBuilder<EcoHelperDbContext>();
            if (useSqlLite)
            {
                builder.UseSqlite("DataSource=:memory:", x => { });
            }
            else
            {
                builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            }

            var dbContext = new EcoHelperDbContext(builder.Options);
            if (useSqlLite)
            {
                dbContext.Database.OpenConnection();
            }

            dbContext.Database.EnsureCreated();

            return dbContext;
        }
    }
}
