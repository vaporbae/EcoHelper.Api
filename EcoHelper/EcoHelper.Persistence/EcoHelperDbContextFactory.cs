namespace EcoHelper.Persistence
{
    using EcoHelper.Persistence.Infrastructure;
    using Microsoft.EntityFrameworkCore;

    public class EcoHelperDbContextFactory : DesignTimeDbContextFactoryBase<EcoHelperDbContext>
    {
        protected override EcoHelperDbContext CreateNewInstance(DbContextOptions<EcoHelperDbContext> options)
        {
            return new EcoHelperDbContext(options);
        }
    }
}
