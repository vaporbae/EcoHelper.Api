namespace EcoHelper.Infrastructure.Repository
{
    using EcoHelper.Application.Interfaces;
    using EcoHelper.Application.Interfaces.Repository;
    using EcoHelper.Domain.Entities;
    using EcoHelper.Infrastructure.Repository.Generic;

    public class BaseVersionsRepository : GenericRepository<BaseVersion, int>, IBaseVersionsRepository
    {
        public BaseVersionsRepository(IEcoHelperDbContext context) : base(context)
        {

        }
    }
}
