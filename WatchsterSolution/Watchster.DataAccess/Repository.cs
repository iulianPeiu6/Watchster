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
            var mappedEntity = context.Set<TEntity>().First(x => x.Id == entity.Id);
            if (mappedEntity != null)
            {
                context.Remove(mappedEntity);
                await context.SaveChangesAsync();
            }
            return mappedEntity;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await context.Set<TEntity>().AsNoTracking().ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            Guard.ArgumentInRange(id, 1, int.MaxValue, nameof(id));

            return await context.FindAsync<TEntity>(id);
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            Guard.ArgumentNotNull(entity, nameof(entity));

            var mappedEntity = context.Set<TEntity>().First(x => x.Id == entity.Id);
            if (mappedEntity != null)
            {
                context.Entry(mappedEntity).CurrentValues.SetValues(entity);
                await context.SaveChangesAsync();
            }
            return mappedEntity;
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
