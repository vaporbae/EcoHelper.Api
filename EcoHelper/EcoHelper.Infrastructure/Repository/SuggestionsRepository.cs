namespace EcoHelper.Infrastructure.Repository
{
    using EcoHelper.Application.Interfaces;
    using EcoHelper.Application.Interfaces.Repository;
    using EcoHelper.Domain.Entities;
    using EcoHelper.Infrastructure.Repository.Generic;

    public class SuggestionsRepository : GenericRepository<Suggestion, int>, ISuggestionsRepository
    {
        public SuggestionsRepository(IEcoHelperDbContext context) : base(context)
        {

        }
    }
}
