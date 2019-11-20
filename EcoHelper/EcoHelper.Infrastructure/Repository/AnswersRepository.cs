namespace EcoHelper.Infrastructure.Repository
{
    using EcoHelper.Application.Interfaces;
    using EcoHelper.Application.Interfaces.Repository;
    using EcoHelper.Domain.Entities;
    using EcoHelper.Infrastructure.Repository.Generic;

    public class AnswersRepository : GenericRepository<Answer, int>, IAnswersRepository
    {
        public AnswersRepository(IEcoHelperDbContext context) : base(context)
        {

        }
    }
}
