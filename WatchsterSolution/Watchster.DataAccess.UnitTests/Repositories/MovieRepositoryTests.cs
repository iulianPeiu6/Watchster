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
        private MovieRepository repository;
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
        public async Task Given_NewMovie_When_NewMovieIsNotNull_Then_AddAsyncShouldAddNewMovie()
        {
            var result = await repository.AddAsync(newMovie);
            var addedMovie = await repository.GetByIdAsync(3);

            result.Should().BeOfType<Movie>();
            addedMovie.Title.Should().Be(newMovie.Title);
            addedMovie.ReleaseDate.Should().Be(newMovie.ReleaseDate);
            addedMovie.Genres.Should().Be(newMovie.Genres);
            addedMovie.Overview.Should().Be(newMovie.Overview);
        }

        [Test]
        public void Given_NewMovie_When_NewMovieIsNull_Then_AddAsyncShouldThrowArgumentNullException()
        {
            Movie newMovieNull = null;

            Action result = () => repository.AddAsync(newMovieNull).Wait();

            result.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public async Task Given_Movie_When_MovieIsInDatabase_Then_DeleteShouldRemoveMovie()
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

            var result = await repository.Delete(movie);
            var deletedMovie = await repository.GetByIdAsync(movie.Id);

            result.Should().NotBeNull();
            result.Should().BeOfType<Movie>();
            deletedMovie.Should().BeNull();
        }

        [Test]
        public async Task Given_MovieDatabase_When_DatabaseIsPopulated_Then_GetAllAsyncShouldReturnAllMovies()
        {
            var result = await repository.GetAllAsync();

            result.Should().BeOfType<List<Movie>>();
            result.Count().Should().Be(1);
        }

        [Test]
        public async Task Given_MovieId_When_MovieIdIsInDatabase_Then_GetByIdAsyncShouldReturnThatMovie()
        {
            var movie = new Movie()
            {
                TMDbId = 2,
                Id = 2,
                Title = "Action-Comedy Movie",
                ReleaseDate = new DateTime(2021, 10, 4),
                Genres = "Crime, Action",
                Overview = "This is a movie for tests, it's genre is Action and Comedy"
            };

            var result = await repository.GetByIdAsync(movie.Id);


            result.Should().BeOfType<Movie>();
            result.Title.Should().Be(movie.Title);
            result.ReleaseDate.Should().Be(movie.ReleaseDate);
            result.Genres.Should().Be(movie.Genres);
            result.Overview.Should().Be(movie.Overview);
        }

        [Test]
        public void Given_MovieId_When_MovieIdIsNull_Then_GetByIdAsyncShouldThrowArgumentException()
        {
            int id = -1;

            Action result = () => repository.GetByIdAsync(id).Wait();

            result.Should().Throw<ArgumentException>();
        }

        [Test]
        public async Task Given_NewMovie_When_MovieWasInDatabase_Then_UpdateAsyncShouldReturnUpdateMovie()
        {
            var movie = new Movie
            {
                TMDbId = 2,
                Id = 2,
                Title = "Different Title",
                ReleaseDate = new DateTime(2021, 10, 4),
                Genres = "Crime, Action",
                Overview = "This is a movie for tests, it's genre is Action and Comedy"
            };

            var result = await repository.UpdateAsync(movie);
            var updatedMovie = await repository.GetByIdAsync(movie.Id);

            updatedMovie.Title.Should().Be(movie.Title);
            updatedMovie.Title.Should().Be(movie.Title);
            updatedMovie.ReleaseDate.Should().Be(movie.ReleaseDate);
            updatedMovie.Genres.Should().Be(movie.Genres);
            updatedMovie.Overview.Should().Be(movie.Overview);
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
            result.Count().Should().Be(1);
        }

        [Test]
        public void Given_Expression_When_MoviesPopulateDatabase_ThenQueryShouldReturnAQueryableCollectionOfMoviesRespectingThatExpression()
        {
            Expression<Func<Movie, bool>> expression = movie => movie.TMDbId < 3;

            var result = repository.Query(expression).ToList();

            result.Should().BeOfType<List<Movie>>();
            result.Count().Should().Be(2);
        }

        [Test]
        public async Task Given_PageNumber_When_PageNumberIsValid_ThenGetMoviesFromPageShouldReturnAListOfMoviesAsync()
        {
            int page = 1;

            var result = await repository.GetMoviesFromPage(page);

            result.Should().BeOfType<List<Movie>>();
            result.Count().Should().Be(2);
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
            result.Result.Should().Be(1);
        }
    }
}
