namespace EcoHelper.Infrastructure.UoW
{
    using System;
    using System.Collections;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using EcoHelper.Application.Interfaces;
    using EcoHelper.Application.Interfaces.Repository;
    using EcoHelper.Application.Interfaces.Repository.Generic;
    using EcoHelper.Application.Interfaces.UoW;
    using EcoHelper.Domain.Entities;
    using EcoHelper.Domain.Entities.Base;
    using EcoHelper.Infrastructure.Repository;
    using EcoHelper.Infrastructure.Repository.Generic;

    public class UnitOfWork : IUnitOfWork
    {
        public IUsersRepository UsersRepository
        {
            get => Repository<UsersRepository, User, int>();
        }
        public IAnswersRepository AnswersRepository
        {
            get => Repository<AnswersRepository, Answer, int>();
        }
        public IDumpstersRepository DumpstersRepository
        {
            get => Repository<DumpstersRepository, Dumpster, int>();
        }
        public IGarbagesRepository GarbagesRepository
        {
            get => Repository<GarbagesRepository, Garbage, int>();
        }
        public IInterestingFactsRepository InterestingFactsRepository
        {
            get => Repository<InterestingFactsRepository, InterestingFact, int>();
        }
        public IQuestionsRepository QuestionsRepository
        {
            get => Repository<QuestionsRepository, Question, int>();
        }
        public IBaseVersionsRepository BaseVersionsRepository
        {
            get => Repository<BaseVersionsRepository, BaseVersion, int>();
        }

        private readonly DbContext _context;

        private bool _disposed;

        private Hashtable _repositories;

        public UnitOfWork(IEcoHelperDbContext context)
        {
            _context = ((DbContext)context);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                ((DbContext)_context).Dispose();
                if (_repositories != null)
                {
                    foreach (IDisposable repository in _repositories.Values)
                    {
                        repository.Dispose();
                    }
                }
            }

            _disposed = true;
        }

        public IGenericRepository<TEntity, int> Repository<TEntity>()
            where TEntity : class, IBaseEntity<int>
        {
            return Repository<TEntity, int>();
        }

        public IGenericRepository<TEntity, TId> Repository<TEntity, TId>()
            where TEntity : class, IBaseEntity<TId>
            where TId : IComparable
        {
            if (_repositories == null)
            {
                _repositories = new Hashtable();
            }

            var type = typeof(TEntity).Name;
            if (_repositories.ContainsKey(type))
            {
                return (IGenericRepository<TEntity, TId>)_repositories[type];
            }

            _repositories.Add(type, Activator.CreateInstance(typeof(GenericRepository<TEntity, TId>), _context));
            return (IGenericRepository<TEntity, TId>)_repositories[type];
        }

        public TSpecificRepository Repository<TSpecificRepository, TEntity, TId>()
            where TSpecificRepository : IGenericReadOnlyRepository<TEntity, TId>
            where TEntity : class, IBaseEntity<TId>
            where TId : IComparable
        {
            if (_repositories == null)
            {
                _repositories = new Hashtable();
            }

            var type = typeof(TEntity).Name;
            if (_repositories.ContainsKey(type))
            {
                return (TSpecificRepository)_repositories[type];
            }

            _repositories.Add(type, Activator.CreateInstance(typeof(TSpecificRepository), _context));
            return (TSpecificRepository)_repositories[type];
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }
    }
}