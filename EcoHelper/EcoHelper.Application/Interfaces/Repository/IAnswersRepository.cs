﻿namespace EcoHelper.Application.Interfaces.Repository
{
    using EcoHelper.Application.Interfaces.Repository.Generic;
    using EcoHelper.Domain.Entities;

    public interface IAnswersRepository : IGenericRepository<Answer, int>
    {
    }
}
