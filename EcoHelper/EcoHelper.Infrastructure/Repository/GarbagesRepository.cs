namespace EcoHelper.Infrastructure.Repository
{
    using EcoHelper.Application.Interfaces;
    using EcoHelper.Application.Interfaces.Repository;
    using EcoHelper.Domain.Entities;
    using EcoHelper.Infrastructure.Repository.Generic;

    public class GarbagesRepository : GenericRepository<Garbage, int>, IGarbagesRepository
    {
        public GarbagesRepository(IEcoHelperDbContext context) : base(context)
        {

        }
    }
}
