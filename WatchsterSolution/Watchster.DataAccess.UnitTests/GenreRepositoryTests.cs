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
    public class GenreRepositoryTests : DatabaseTestBase
    {
        private readonly Repository<Genre> repository;
        private readonly Genre newGenre;

        public GenreRepositoryTests()
        {
            repository = new Repository<Genre>(context);
            newGenre = new Genre()
            {
                Id = Guid.Parse("175fb27d-cc8d-4db7-acbc-70d5301f511e"),
                TMDbId = 3,
                Name = "Romance"
            };
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Given_NewGenre_When_NewGenreIsNotNull_Then_AddAsyncShouldReturnATaskConcerningNewGenre()
        {
            var result = repository.AddAsync(newGenre);

            result.Should().BeOfType<Task<Genre>>();
        }

        [Test]
        public void Given_NewGenre_When_NewGenreIsNull_Then_AddAsyncShouldThrowArgumentNullException()
        {
            Genre newGenreNull = null;

            Action result = () => repository.AddAsync(newGenreNull).Wait();

            result.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Given_Genre_When_GenreIsInDatabase_Then_DeleteShouldReturnATaskConcerningDeletedGenre()
        {
            var genre = new Genre
            {
                Id = Guid.Parse("9f05681f-80a7-4d69-8e0a-3dcf65a87919"),
                TMDbId = 1,
                Name = "Action"
            };

            var result = repository.Delete(genre);

            result.Should().BeOfType<Task<Genre>>();
        }

        [Test]
        public void Given_GenreDatabase_When_DatabaseIsPopulated_Then_GetAllAsyncShouldReturnATaskConcerningAllGenres()
        {
            var result = repository.GetAllAsync();

            result.Should().BeOfType<Task<IEnumerable<Genre>>>();
        }

        [Test]
        public void Given_GenreId_When_GenreIdIsInDatabase_Then_GetByIdAsyncShouldReturnATaskConcerningThatGenre()
        {
            var id = Guid.Parse("9f05681f-80a7-4d69-8e0a-3dcf65a87919");

            var result = repository.GetByIdAsync(id);

            result.Should().BeOfType<Task<Genre>>();
        }

        [Test]
        public void Given_GenreId_When_GenreIdIsNull_Then_GetByIdAsyncShouldThrowArgumentException()
        {
            Guid id = Guid.Empty;

            Action result = () => repository.GetByIdAsync(id).Wait();

            result.Should().Throw<ArgumentException>();
        }

        [Test]
        public void Given_NewGenre_When_GenreWasInDatabase_Then_UpdateAsyncShouldReturnATaskConcerningUpdatedGenre()
        {
            var result = repository.UpdateAsync(newGenre);

            result.Should().BeOfType<Task<Genre>>();
        }

        [Test]
        public void Given_NewGenre_When_GenreIsNull_Then_UpdateAsyncShouldThrowArgumentNullException()
        {
            Genre newGenreNull = null;

            Action result = () => repository.UpdateAsync(newGenreNull).Wait();

            result.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Given_Genres_When_GenresPopulateDatabase_Then_QueryShouldReturnAQueryableCollectionOfGenres()
        {
            var result = repository.Query().ToList();

            result.Should().BeOfType<List<Genre>>();
        }

        [Test]
        public void Given_Expression_When_GenresPopulateDatabase_ThenQueryShouldReturnAQueryableCollectionOfGenresRespectingThatExpression()
        {
            Expression<Func<Genre, bool>> expression = Genre => Genre.TMDbId < 3;

            var result = repository.Query(expression).ToList();

            result.Should().BeOfType<List<Genre>>();
        }
    }
}
