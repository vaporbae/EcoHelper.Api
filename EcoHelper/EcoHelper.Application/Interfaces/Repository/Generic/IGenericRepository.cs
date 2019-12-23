namespace EcoHelper.Application.Interfaces.Repository.Generic
{
    using EcoHelper.Domain.Entities.Base;
    using System;
    using System.Threading.Tasks;

    public interface IGenericRepository<TEntity, TId> : IGenericReadOnlyRepository<TEntity, TId>
        where TEntity : class, IBaseEntity<TId> where TId : IComparable
    {
        TEntity Add(TEntity entity);

        void Update(TEntity entity);

        Task Remove(TId id);

        void Remove(TEntity entity);

        Task SaveAsync();
    }
}