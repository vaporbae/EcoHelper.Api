namespace EcoHelper.Persistence
{
    using Microsoft.EntityFrameworkCore;
    using EcoHelper.Application.Interfaces;
    using EcoHelper.Domain.Entities;

    public class EcoHelperDbContext : DbContext, IEcoHelperDbContext
    {
        public EcoHelperDbContext(DbContextOptions<EcoHelperDbContext> options) : base(options)
        {

        }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EcoHelperDbContext).Assembly);
        }
    }
}
