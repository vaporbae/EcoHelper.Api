namespace EcoHelper.Infrastructure.Repository
{
    using EcoHelper.Application.Interfaces;
    using EcoHelper.Application.Interfaces.Repository;
    using EcoHelper.Domain.Entities;
    using EcoHelper.Infrastructure.Repository.Generic;

    public class InterestingFactsRepository : GenericRepository<InterestingFact, int>, IInterestingFactsRepository
    {
        public InterestingFactsRepository(IEcoHelperDbContext context) : base(context)
        {

        }
    }
}
