using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Watchster.DataAccess;
using Watchster.Domain.Entities;

namespace Database.UnitTests
{
    public class MovieRepositoryTests : DatabaseTestBase
    {
        private readonly Repository<Movie> repository;
        private readonly Movie newMovie;

        public MovieRepositoryTests()
        {
            repository = new Repository<Movie>(context);
            Genre genre = new Genre()
            {
                Id = Guid.Parse("175fb27d-cc8d-4db7-acbc-70d5301f511e"),
                TMDbId = 3,
                Name = "Romance"
            };
            List<Genre> newMovieGenres = new List<Genre>();
            newMovieGenres.Add(genre);
            newMovie = new Movie
            {
                Id = Guid.Parse("85abe305-307b-4b7f-81cc-cf5a24b1aaf7"),
                TMDbId = 3,
                Title = "New Movie Title",
                ReleaseDate = new DateTime(2010, 9, 9),
                Genres = newMovieGenres,
                Overview = "Unit test movie"
            };
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Given_NewMovie_When_NewMovieIsNotNull_Then_AddAsyncShouldReturnATaskConcerningNewMovie()
        {
            var result = repository.AddAsync(newMovie);

            result.Should().BeOfType<Task<Movie>>();
        }

        [Test]
        public void Given_NewMovie_When_NewMovieIsNull_Then_AddAsyncShouldThrowArgumentNullException()
        {
            Movie newMovieNull = null;

            Action result = () => repository.AddAsync(newMovieNull).Wait();

            result.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Given_Movie_When_MovieIsInDatabase_Then_DeleteShouldReturnATaskConcerningDeletedMovie()
        {
            var genre = new Genre
            {
                Id = Guid.Parse("9f05681f-80a7-4d69-8e0a-3dcf65a87919"),
                TMDbId = 1,
                Name = "Action"
            };

            List<Genre> Movie1Genres = new List<Genre>();
            Movie1Genres.Add(genre);

            var movie = new Movie
            {
                TMDbId = 1,
                Id = Guid.Parse("1e8a1085-1b1f-4c7c-b630-7086732f7ffc"),
                Title = "Action Movie",
                ReleaseDate = new DateTime(2009, 5, 5),
                Genres = Movie1Genres,
                Overview = "This is a movie for tests, it's genre is only Action"
            };

            var result = repository.Delete(movie);

            result.Should().BeOfType<Task<Movie>>();
        }

        [Test]
        public void Given_MovieDatabase_When_DatabaseIsPopulated_Then_GetAllAsyncShouldReturnATaskConcerningAllMovies()
        {
            var result = repository.GetAllAsync();

            result.Should().BeOfType<Task<IEnumerable<Movie>>>();
        }

        [Test]
        public void Given_MovieId_When_MovieIdIsInDatabase_Then_GetByIdAsyncShouldReturnATaskConcerningThatMovie()
        {
            var id = Guid.Parse("1e8a1085-1b1f-4c7c-b630-7086732f7ffc");

            var result = repository.GetByIdAsync(id);

            result.Should().BeOfType<Task<Movie>>();
        }

        [Test]
        public void Given_MovieId_When_MovieIdIsNull_Then_GetByIdAsyncShouldThrowArgumentException()
        {
            Guid id = Guid.Empty;

            Action result = () => repository.GetByIdAsync(id).Wait();

            result.Should().Throw<ArgumentException>();
        }

        [Test]
        public void Given_NewMovie_When_MovieWasInDatabase_Then_UpdateAsyncShouldReturnATaskConcerningUpdatedMovie()
        {
            var result = repository.UpdateAsync(newMovie);

            result.Should().BeOfType<Task<Movie>>();
        }

        [Test]
        public void Given_NewMovie_When_MovieIsNull_Then_UpdateAsyncShouldThrowArgumentNullException()
        {
            Movie newMovieNull = null;

            Action result = () => repository.UpdateAsync(newMovieNull).Wait();

            result.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Given_Movies_When_MoviesPopulateDatabase_Then_QueryShouldReturnAQueryableCollectionOfMovies()
        {
            var result = repository.Query().ToList();

            result.Should().BeOfType<List<Movie>>();
        }

        [Test]
        public void Given_Expression_When_MoviesPopulateDatabase_ThenQueryShouldReturnAQueryableCollectionOfMoviesRespectingThatExpression()
        {
            Expression<Func<Movie,bool>> expression = movie => movie.TMDbId < 3;

            var result = repository.Query(expression).ToList();

            result.Should().BeOfType<List<Movie>>();
        }
    }
}
