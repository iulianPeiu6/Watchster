using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Watchster.Application.Interfaces;
using Watchster.Domain.Entities;
using Faker;

namespace Watchster.Application.UnitTests.Fakes
{
    public class FakeMovieRepository : IMovieRepository, IQueryable<Movie>
    {
        private List<Movie> entities = null;

        public FakeMovieRepository(IEnumerable<Movie> collection)
        {
            this.entities = new List<Movie>(collection);
        }

        public IEnumerator<Movie> GetEnumerator()
        {
            return entities.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return entities.GetEnumerator();
        }

        public Task<Movie> AddAsync(Movie entity)
        {
            entities.Add(entity);
            return Task.Run(() => entity);
        }

        public Task<Movie> UpdateAsync(Movie entity)
        {
            throw new NotImplementedException();
        }

        public Task<Movie> Delete(Movie entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Movie>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Movie> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                return Task.Run(() => default(Movie));
            }
            else
            {
                return Task.Run(() => new Movie()
                {
                    Id = RandomNumber.Next(),
                    Genres = Lorem.Sentence(),
                    Overview = Lorem.Sentence(),
                    Popularity = RandomNumber.Next(),
                    PosterUrl = Internet.Url(),
                    ReleaseDate = DateTime.Now,
                    Title = Lorem.Sentence(),
                    TMDbId = RandomNumber.Next(),
                    TMDbVoteAverage = RandomNumber.Next(),
                });
            }
        }

        public IQueryable<Movie> Query()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Movie> Query(Expression<Func<Movie, bool>> expression)
        {
            return entities.Where(expression.Compile()).AsQueryable();
        }

        public Task<IList<Movie>> GetMoviesFromPage(int page)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetTotalPages()
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
