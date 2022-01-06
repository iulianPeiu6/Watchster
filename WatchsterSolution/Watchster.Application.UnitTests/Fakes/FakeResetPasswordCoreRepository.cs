using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Watchster.Aplication.Interfaces;
using Watchster.Application.Interfaces;
using Watchster.Application.Models;
using Watchster.Domain.Entities;

namespace Watchster.Application.UnitTests.Fakes
{
    public class FakeResetPasswordCodeRepository : IResetPasswordCodeRepository, IQueryable<ResetPasswordCode>
    {
        private List<ResetPasswordCode> entities = null;
        private const int minValue = 1;
        private const int maxValue = int.MaxValue;
        public FakeResetPasswordCodeRepository(IEnumerable<ResetPasswordCode> collection)
        {
            this.entities = new List<ResetPasswordCode>(collection);
            var code = new ResetPasswordCode
            {
                Code = "hardcoded",
                Email = "emai@emai.email",
                expirationDate = DateTime.Now,
                Id = 0
            };
            var code2 = new ResetPasswordCode
            {
                Code = "code without email",
                expirationDate = DateTime.MinValue,
            };
            entities.Add(code);
            entities.Add(code2);
        }

        public IEnumerator<ResetPasswordCode> GetEnumerator()
        {
            return entities.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return entities.GetEnumerator();
        }

        public Task<ResetPasswordCode> AddAsync(ResetPasswordCode entity)
        {
            entities.Add(entity);
            return Task.Run(() => entity);
        }

        public Task<ResetPasswordCode> UpdateAsync(ResetPasswordCode entity)
        {
            throw new NotImplementedException();
        }

        public Task<ResetPasswordCode> Delete(ResetPasswordCode entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ResetPasswordCode>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ResetPasswordCode> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
 
        }

        public IQueryable<ResetPasswordCode> Query()
        {
            throw new NotImplementedException();
        }

        public IQueryable<ResetPasswordCode> Query(Expression<Func<ResetPasswordCode, bool>> expression)
        {
            return entities.Where(expression.Compile()).AsQueryable();
        }
 

        Task<IEnumerable<ResetPasswordCode>> IRepository<ResetPasswordCode>.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        Task<ResetPasswordCode> IRepository<ResetPasswordCode>.GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        IQueryable<ResetPasswordCode> IRepository<ResetPasswordCode>.Query()
        {
            throw new NotImplementedException();
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
