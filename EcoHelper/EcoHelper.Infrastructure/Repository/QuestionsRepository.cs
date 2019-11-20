namespace EcoHelper.Infrastructure.Repository
{
    using EcoHelper.Application.Interfaces;
    using EcoHelper.Application.Interfaces.Repository;
    using EcoHelper.Domain.Entities;
    using EcoHelper.Infrastructure.Repository.Generic;

    public class QuestionsRepository : GenericRepository<Question, int>, IQuestionsRepository
    {
        public QuestionsRepository(IEcoHelperDbContext context) : base(context)
        {

        }
    }
}
