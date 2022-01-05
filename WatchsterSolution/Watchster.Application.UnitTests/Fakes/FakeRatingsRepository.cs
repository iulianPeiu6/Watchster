using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Watchster.Application.Interfaces;
using Watchster.Domain.Entities;

namespace Watchster.Application.UnitTests.Fakes
{
    public class FakeRatingsRepository : IRatingRepository, IQueryable<Rating>
    {
        private List<Rating> entities = null;

        public FakeRatingsRepository(IEnumerable<Rating> collection)
        {
            this.entities = new List<Rating>(collection);
        }

        public IEnumerator<Rating> GetEnumerator()
        {
            return entities.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return entities.GetEnumerator();
        }

        public Task<Rating> AddAsync(Rating entity)
        {
            entities.Add(entity);
            return Task.Run(() => entity);
        }

        public Task<Rating> UpdateAsync(Rating entity)
        {
            throw new NotImplementedException();
        }

        public Task<Rating> Delete(Rating entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Rating>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Rating> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Rating> Query()
        {
            return entities.AsQueryable();
        }

        public IQueryable<Rating> Query(Expression<Func<Rating, bool>> expression)
        {
            return entities.Where(expression.Compile()).AsQueryable();
        }
        public Type ElementType
        {
            get { return this.entities.AsQueryable().ElementType; }
        }

        public Expression Expression
        {
            get { return this.entities.AsQueryable().Expression; }
        }

        public IQueryProvider Provider
        {
            get { return this.entities.AsQueryable().Provider; }
        }
    }
}