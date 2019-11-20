﻿namespace EcoHelper.Application.Interfaces.UoW
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using EcoHelper.Application.Interfaces.Repository;
    using EcoHelper.Application.Interfaces.Repository.Generic;
    using EcoHelper.Domain.Entities.Base;

    public interface IUnitOfWork : IDisposable
    {
        IUsersRepository UsersRepository { get; }

        void Dispose(bool disposing);

        IGenericRepository<TEntity, int> Repository<TEntity>() where TEntity : class, IBaseEntity<int>;

        IGenericRepository<TEntity, TId> Repository<TEntity, TId>() where TEntity : class, IBaseEntity<TId> where TId : IComparable;

        int SaveChanges();

        Task<int> SaveChangesAsync();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}

