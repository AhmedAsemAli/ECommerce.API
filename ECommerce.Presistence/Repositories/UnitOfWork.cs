using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities;
using ECommerce.Presistence.Data.DbContexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Presistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext _dbContext;
        private readonly Dictionary<Type, object> _repositories = [];
        public UnitOfWork(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            var entityType=typeof(TEntity);
            if (_repositories.TryGetValue(entityType,out var repository ))
            {
                return (IGenericRepository<TEntity,TKey>)repository;
            }

            var newRepo = new GenericRepository<TEntity, TKey>(_dbContext);
            _repositories[entityType]=newRepo;
           return newRepo;
        }

        public async Task<int> SaveChangesAsync()
        {
          return await _dbContext.SaveChangesAsync();
        }
    }
}
