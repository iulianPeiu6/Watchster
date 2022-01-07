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
    public class FakeRatingRepository : IRatingRepository, IQueryable<Rating>
    {
        private List<Rating> entities = null;

        public FakeRatingRepository(IEnumerable<Rating> collection)
        {
            this.entities = new List<Rating>(collection);
            var rating = new Rating
            {
                Id = 1,
                MovieId = 1,
                UserId = 1,
                RatingValue = 1,
            };
            entities.Add(rating);
        }

        public IEnumerator<Rating> GetEnumerator()
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
            if (id <= 0)
            {
                return Task.Run(() => default(Rating));
            }
            else
            {
                return Task.Run(() => new Rating()
                {
                    Id = RandomNumber.Next(),
                    MovieId = RandomNumber.Next(),
                    RatingValue = RandomNumber.Next(),
                    UserId = RandomNumber.Next(),
                });
            }
        }

        public IQueryable<Rating> Query()
        {
            throw new NotImplementedException();
        }

        private object GetValue(MemberExpression member)
        {
            var objectMember = Expression.Convert(member, typeof(object));

            var getterLambda = Expression.Lambda<Func<object>>(objectMember);

            var getter = getterLambda.Compile();

            return getter();
        }

        public IQueryable<Rating> Query(Expression<Func<Rating, bool>> expression)
        {
            return entities.Where(expression.Compile()).AsQueryable();
        }

        IEnumerator IEnumerable.GetEnumerator()
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
