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
        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<Dumpster> Dumpsters { get; set; }
        public virtual DbSet<Garbage> Garbages { get; set; }
        public virtual DbSet<InterestingFact> InterestingFacts { get; set; }
        public virtual DbSet<Question> Questions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EcoHelperDbContext).Assembly);
        }
    }
}
