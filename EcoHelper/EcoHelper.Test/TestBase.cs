﻿namespace EcoHelper.Test
{
    using EcoHelper.Persistence;
    using Microsoft.EntityFrameworkCore;
    using System;

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
