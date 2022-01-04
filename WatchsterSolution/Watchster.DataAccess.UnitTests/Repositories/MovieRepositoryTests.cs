using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Watchster.DataAccess.Repositories;
using Watchster.Domain.Entities;

namespace Database.UnitTests
{
    public class MovieRepositoryTests : DatabaseTestBase
    {
        private readonly MovieRepository repository;
        private readonly Movie newMovie;

        public MovieRepositoryTests()
        {
            repository = new MovieRepository(context);
            newMovie = new Movie
            {
                Id = 3,
                TMDbId = 3,
                Title = "New Movie Title",
                ReleaseDate = new DateTime(2010, 9, 9),
                Genres = "Crime, Action",
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
            var movie = new Movie
            {
                TMDbId = 1,
                Id = 1,
                Title = "Action Movie",
                ReleaseDate = new DateTime(2009, 5, 5),
                Genres = "Crime, Action",
                Overview = "This is a movie for tests, it's genre is only Action"
            };

            var result = repository.Delete(movie);
            result.Should().NotBeNull();
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
            var id = 1;

            var result = repository.GetByIdAsync(id);

            result.Should().BeOfType<Task<Movie>>();
        }

        [Test]
        public void Given_MovieId_When_MovieIdIsNull_Then_GetByIdAsyncShouldThrowArgumentException()
        {
            int id = -1;

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
            Expression<Func<Movie, bool>> expression = movie => movie.TMDbId < 3;

            var result = repository.Query(expression).ToList();

            result.Should().BeOfType<List<Movie>>();
        }

        [Test]
        public void Given_PageNumber_When_PageNumberIsValid_ThenGetMoviesFromPageShouldReturnAListOfMovies()
        {
            int page = 2;

            var result = repository.GetMoviesFromPage(page);
            result.Should().BeOfType<Task<IList<Movie>>>();
        }

        [Test]
        public void Given_PageNumber_When_PageNumberIsInvalid_ThenGetMoviesFromPageShouldThrowArgumentException()
        {
            int page = -1;

            Action result = () => repository.GetMoviesFromPage(page).Wait();
            result.Should().Throw<ArgumentException>();
        }

        [Test]

        public void Given_GetTotalPagesCall_When_TestsHaveLessThan2000Movies_ThenShouldReturnZero()
        {
            var result = repository.GetTotalPages();
            result.Result.Should().Equals(0);
        }
    }
}
