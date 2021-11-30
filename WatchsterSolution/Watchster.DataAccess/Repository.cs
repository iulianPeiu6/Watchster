using System;
using System.Collections.Generic;
using System.Linq;
using Watchster.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Watchster.DataAccess.Context;
using Watchster.DataAccess.Interfaces;

namespace Watchster.DataAccess
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly WatchsterContext watchsterContext;

        private readonly DbSet<TEntity> dbSet;

        public Repository(WatchsterContext watchsterContext)
        {
            this.watchsterContext = watchsterContext;
            dbSet = watchsterContext.Set<TEntity>();
        }
        public TEntity Add(TEntity entity)
        {
            return dbSet.Add(entity).Entity;
        }

        public TEntity Update(TEntity entity)
        {
            var updatedEntity = dbSet.Attach(entity).Entity;
            watchsterContext.Entry(entity).State = EntityState.Modified;
            return updatedEntity;
        }

        public void Delete(TEntity entity)
        {
            dbSet.Remove(entity);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return dbSet.ToList();
        }

        public TEntity GetById(Guid id)
        {
            return dbSet.Find(id);
        }

        public IQueryable<TEntity> Query()
        {
            return dbSet.AsQueryable();
        }

        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> expression)
        {
            return dbSet.Where(expression).AsQueryable();
        }
    }
}
