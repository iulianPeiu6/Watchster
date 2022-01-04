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
    public class RatingRepositoryTests : DatabaseTestBase
    {
        private readonly RatingRepository repository;
        private readonly Rating newRating;

        public RatingRepositoryTests()
        {
            repository = new RatingRepository(context);

            newRating = new Rating
            {
                Id = 3,
                UserId = 1,
                MovieId = 2,
                RatingValue = 7.3
            };
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task Given_NewRating_When_NewRatingIsNotNull_Then_AddAsyncShouldReturnAddNewRating()
        {
            var result = await repository.AddAsync(newRating);
            var addedRating = await repository.GetByIdAsync(newRating.Id);

            result.Should().BeOfType<Rating>();
            addedRating.UserId.Should().Be(newRating.UserId);
            addedRating.MovieId.Should().Be(newRating.MovieId);
            addedRating.RatingValue.Should().Be(newRating.RatingValue);
        }

        [Test]
        public void Given_NewRating_When_NewRatingIsNull_Then_AddAsyncShouldThrowArgumentNullException()
        {
            Rating newRatingNull = null;

            Action result = () => repository.AddAsync(newRatingNull).Wait();

            result.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public async Task Given_Rating_When_RatingIsInDatabase_Then_DeleteShouldReturnDeleteRating()
        {
            var rating = new Rating
            {
                Id = 1,
                UserId = 1,
                MovieId = 1,
                RatingValue = 8.9
            };

            var result = await repository.Delete(rating);
            var deletedRating = await repository.GetByIdAsync(rating.Id);

            result.Should().BeOfType<Rating>();
            deletedRating.Should().BeNull();
        }

        [Test]
        public async Task Given_RatingDatabase_When_DatabaseIsPopulated_Then_GetAllAsyncShouldReturnAllRatings()
        {
            var result = await repository.GetAllAsync();

            result.Should().BeOfType<List<Rating>>();
            result.Count().Should().Be(2);
        }

        [Test]
        public async Task Given_RatingId_When_RatingIdIsInDatabase_Then_GetByIdAsyncShouldReturnThatRating()
        {
            var result = await repository.GetByIdAsync(newRating.Id);

            result.Should().BeOfType<Rating>();
            result.UserId.Should().Be(newRating.UserId);
            result.MovieId.Should().Be(newRating.MovieId);
            result.RatingValue.Should().Be(newRating.RatingValue);
        }

        [Test]
        public void Given_RatingId_When_RatingIdIsNull_Then_GetByIdAsyncShouldThrowArgumentException()
        {
            var id = -1;

            Action result = () => repository.GetByIdAsync(id).Wait();

            result.Should().Throw<ArgumentException>();
        }

        [Test]
        public async Task Given_NewRating_When_RatingWasInDatabase_Then_UpdateAsyncShouldUpdateRating()
        {
            var rating = new Rating
            {
                Id = 2,
                UserId = 2,
                MovieId = 1,
                RatingValue = 10
            };
            var result = await repository.UpdateAsync(rating);
            var updatedRating = await repository.GetByIdAsync(rating.Id);

            result.Should().BeOfType<Rating>();
            updatedRating.RatingValue.Should().Be(rating.RatingValue);
        }

        [Test]
        public void Given_NewRating_When_RatingIsNull_Then_UpdateAsyncShouldThrowArgumentNullException()
        {
            Rating newRatingNull = null;

            Action result = () => repository.UpdateAsync(newRatingNull).Wait();

            result.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Given_Ratings_When_RatingsPopulateDatabase_Then_QueryShouldReturnAQueryableCollectionOfRatings()
        {
            var result = repository.Query().ToList();

            result.Should().BeOfType<List<Rating>>();
            result.Count().Should().Be(2);
        }

        [Test]
        public void Given_Expression_When_RatingsPopulateDatabase_ThenQueryShouldReturnAQueryableCollectionOfRatingsRespectingThatExpression()
        {
            Expression<Func<Rating, bool>> expression = Rating => Rating.RatingValue > 8.9;

            var result = repository.Query(expression).ToList();

            result.Should().BeOfType<List<Rating>>();
            result.Count().Should().Be(1);
        }
    }
}
