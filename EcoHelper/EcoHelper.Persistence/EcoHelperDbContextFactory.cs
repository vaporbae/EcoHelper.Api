namespace EcoHelper.Persistence
{
    using Microsoft.EntityFrameworkCore;
    using EcoHelper.Persistence.Infrastructure;

    public class EcoHelperDbContextFactory : DesignTimeDbContextFactoryBase<EcoHelperDbContext>
    {
        protected override EcoHelperDbContext CreateNewInstance(DbContextOptions<EcoHelperDbContext> options)
        {
            return new EcoHelperDbContext(options);
        }
    }
}
