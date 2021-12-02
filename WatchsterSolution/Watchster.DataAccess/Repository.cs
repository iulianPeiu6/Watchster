using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Watchster.Aplication.Interfaces;
using Watchster.DataAccess.Context;
using Watchster.Domain.Common;

namespace Watchster.DataAccess
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly WatchsterContext context;

        private readonly DbSet<TEntity> dbSet;

        public Repository(WatchsterContext context)
        {
            this.context = context;
            dbSet = context.Set<TEntity>();
        }
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(AddAsync)} entity must not be null");
            }
            await context.AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> Delete(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(Delete)} entity mult not be null");
            }

            context.Remove(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await context.Set<TEntity>().AsNoTracking().ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException($"{nameof(GetByIdAsync)} id must not be empty");
            }

            return await context.FindAsync<TEntity>(id);
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(UpdateAsync)} entity must not be null");
            }

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
