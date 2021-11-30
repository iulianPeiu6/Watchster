using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Watchster.Domain.Common;

namespace Watchster.DataAccess.Interfaces
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        TEntity Add(TEntity entity);

        TEntity Update(TEntity entity);

        void Delete(TEntity entity);

        IEnumerable<TEntity> GetAll();

        TEntity GetById(Guid id);

        IQueryable<TEntity> Query();

        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> expression);
    }
}
