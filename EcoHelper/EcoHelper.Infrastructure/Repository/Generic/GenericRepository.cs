﻿namespace EcoHelper.Infrastructure.Repository.Generic
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using EcoHelper.Application.Interfaces.Repository.Generic;
    using EcoHelper.Application.Interfaces;
    using EcoHelper.Domain.Entities.Base;

    public class GenericRepository<TEntity, TId> : GenericReadOnlyRepository<TEntity, TId>, IGenericRepository<TEntity, TId>
        where TEntity : class, IBaseEntity<TId> where TId : IComparable
    {
        public GenericRepository(IEcoHelperDbContext context) : base(context)
        {

        }

        public virtual TEntity Add(TEntity entity)
        {
            DateTime time = DateTime.UtcNow;
            entity.Created = time;
            entity.Modified = time;

            var createdEntity = _dbSet.Add(entity);

            return createdEntity.Entity;
        }

        public virtual void Update(TEntity entity)
        {
            entity.Modified = DateTime.UtcNow;

            _dbSet.Attach(entity);
            ((DbContext)_context).Entry(entity).State = EntityState.Modified;
        }

        public virtual async Task Remove(TId id)
        {
            TEntity entity = await _dbSet.FindAsync(id);
            Remove(entity);
        }

        public virtual void Remove(TEntity entity)
        {
            if (((DbContext)_context).Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }

            _dbSet.Remove(entity);
        }

        public virtual Task SaveAsync()
        {
            return ((DbContext)_context).SaveChangesAsync();
        }
    }
}
