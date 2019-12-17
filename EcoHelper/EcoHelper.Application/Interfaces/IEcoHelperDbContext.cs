
namespace EcoHelper.Application.Interfaces
{
    using Microsoft.EntityFrameworkCore;
    using EcoHelper.Domain.Entities;

    public interface IEcoHelperDbContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Answer> Answers { get; set; }
        DbSet<Dumpster> Dumpsters { get; set; }
        DbSet<Garbage> Garbages { get; set; }
        DbSet<InterestingFact> InterestingFacts { get; set; }
        DbSet<Question> Questions { get; set; }
        DbSet<BaseVersion> BaseVersions { get; set; }
        DbSet<Suggestion> Suggestions { get; set; }
    }
}
