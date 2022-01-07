using Faker;
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
    public class FakeUserRepository : IUserRepository, IQueryable<User>
    {
        private List<User> entities = null;
        private const int minValue = 1;
        private const int maxValue = int.MaxValue;
        public FakeUserRepository(IEnumerable<User> collection)
        {
            this.entities = new List<User>(collection);
            var user = new User
            {
                Email = "emai@emai.email"
            };
            entities.Add(user);
        }

        public IEnumerator<User> GetEnumerator()
        {
            return entities.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return entities.GetEnumerator();
        }

        public Task<User> AddAsync(User entity)
        {
            entities.Add(entity);
            return Task.Run(() => entity);
        }

        public Task<User> UpdateAsync(User entity)
        {
            return Task.Run(() => entity);
        }

        public Task<User> Delete(User entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                return Task.Run(() => default(User));
            }
            else
            {
                return Task.Run(() => new User()
                {
                    Email = Internet.Email(),
                    Id = RandomNumber.Next(),
                    IsSubscribed = RandomNumber.Next() % 2 == 0,
                    Password = Lorem.Sentence(),
                    UserRatings = null,
                    RegistrationDate = DateTime.Now
                });
            }
        }

        public IQueryable<User> Query()
        {
            throw new NotImplementedException();
        }

        public IQueryable<User> Query(Expression<Func<User, bool>> expression)
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
