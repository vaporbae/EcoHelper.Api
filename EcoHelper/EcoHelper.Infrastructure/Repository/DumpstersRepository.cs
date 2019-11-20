namespace EcoHelper.Infrastructure.Repository
{
    using EcoHelper.Application.Interfaces;
    using EcoHelper.Application.Interfaces.Repository;
    using EcoHelper.Domain.Entities;
    using EcoHelper.Infrastructure.Repository.Generic;

    public class DumpstersRepository : GenericRepository<Dumpster, int>, IDumpstersRepository
    {
        public DumpstersRepository(IEcoHelperDbContext context) : base(context)
        {

        }
    }
}
