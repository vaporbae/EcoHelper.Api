namespace EcoHelper.Infrastructure.Repository
{
    using EcoHelper.Application.Interfaces;
    using EcoHelper.Application.Interfaces.Repository;
    using EcoHelper.Domain.Entities;
    using EcoHelper.Infrastructure.Repository.Generic;

    public class UsersRepository : GenericRepository<User, int>, IUsersRepository
    {
        public UsersRepository(IEcoHelperDbContext context) : base(context)
        {

        }
    }
}
