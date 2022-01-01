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
    public class FakeAppSettingsRepository : IAppSettingsRepository, IQueryable<AppSettings>
    {
        private List<AppSettings> entities = null;

        public FakeAppSettingsRepository(IEnumerable<AppSettings> collection)
        {
            this.entities = new List<AppSettings>(collection);
        }

        public IEnumerator<AppSettings> GetEnumerator()
        {
            return entities.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return entities.GetEnumerator();
        }

        public Task<AppSettings> AddAsync(AppSettings entity)
        {
            entities.Add(entity);
            return Task.Run(() => entity);
        }

        public Task<AppSettings> UpdateAsync(AppSettings entity)
        {
            throw new NotImplementedException();
        }

        public Task<AppSettings> UpdateAsync(AppSettings entity, Func<AppSettings, object> modify)
        {
            throw new NotImplementedException();
        }

        public Task<AppSettings> Delete(AppSettings entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AppSettings>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<AppSettings> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<AppSettings> Query()
        {
            throw new NotImplementedException();
        }

        public IQueryable<AppSettings> Query(Expression<Func<AppSettings, bool>> expression)
        {
            return entities.AsQueryable();
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
