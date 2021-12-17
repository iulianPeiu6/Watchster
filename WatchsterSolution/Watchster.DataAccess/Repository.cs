using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Vanguard;
using Watchster.Aplication.Interfaces;
using Watchster.DataAccess.Context;
using Watchster.Domain.Common;

namespace Watchster.DataAccess
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly WatchsterContext context;

        protected readonly DbSet<TEntity> dbSet;

        public Repository(WatchsterContext context)
        {
            this.context = context;
            dbSet = context.Set<TEntity>();
        }
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            Guard.ArgumentNotNull(entity, nameof(entity));

            await context.AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> Delete(TEntity entity)
        {
            Guard.ArgumentNotNull(entity, nameof(entity));

            context.Remove(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await context.Set<TEntity>().AsNoTracking().ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            Guard.ArgumentInRange(id, 1,int.MaxValue, nameof(id));

            return await context.FindAsync<TEntity>(id);
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            Guard.ArgumentNotNull(entity, nameof(entity));

            var updatedEntity = dbSet.Attach(entity).Entity;
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return updatedEntity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity, Func<TEntity, object> modify)
        {
            foreach (PropertyInfo p in modify(entity).GetType().GetProperties())
            {
                context.Entry(entity).Property(p.Name).IsModified = true;
            }
            await context.SaveChangesAsync();
            return entity;
        }

        public IQueryable<TEntity> Query()
        {
            return dbSet.AsNoTracking().AsQueryable();
        }

        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> expression)
        {
            return dbSet.AsNoTracking().Where(expression).AsQueryable();
        }
    }
}
