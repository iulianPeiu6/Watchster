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
    public class RatingRepositoryTests : DatabaseTestBase
    {
        private readonly Repository<Rating> repository;
        private readonly Rating newRating;

        public RatingRepositoryTests()
        {
            repository = new Repository<Rating>(context);

            var rating = new Rating
            {
                Id = Guid.Parse("94b44832-3990-4508-8d6f-c2a6eb831fa9"),
                UserId = Guid.Parse("56db7a89-9b32-4f36-ba80-b3ac182cef53"),
                MovieId = Guid.Parse("1e8a1085-1b1f-4c7c-b630-7086732f7ffc"),
                RatingValue = 7.3
            };
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Given_NewRating_When_NewRatingIsNotNull_Then_AddAsyncShouldReturnATaskConcerningNewRating()
        {
            var result = repository.AddAsync(newRating);

            result.Should().BeOfType<Task<Rating>>();
        }

        [Test]
        public void Given_NewRating_When_NewRatingIsNull_Then_AddAsyncShouldThrowArgumentNullException()
        {
            Rating newRatingNull = null;

            Action result = () => repository.AddAsync(newRatingNull).Wait();

            result.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Given_Rating_When_RatingIsInDatabase_Then_DeleteShouldReturnATaskConcerningDeletedRating()
        {
            var rating = new Rating
            {
                Id = Guid.Parse("eeb15cc4-11c5-4897-a11d-2beca00abb81"),
                UserId = Guid.Parse("56db7a89-9b32-4f36-ba80-b3ac182cef53"),
                MovieId = Guid.Parse("121fdd7c-6671-4db9-b631-5145667287d8"),
                RatingValue = 8.9
            };

            var result = repository.Delete(rating);

            result.Should().BeOfType<Task<Rating>>();
        }

        [Test]
        public void Given_RatingDatabase_When_DatabaseIsPopulated_Then_GetAllAsyncShouldReturnATaskConcerningAllRatings()
        {
            var result = repository.GetAllAsync();

            result.Should().BeOfType<Task<IEnumerable<Rating>>>();
        }

        [Test]
        public void Given_RatingId_When_RatingIdIsInDatabase_Then_GetByIdAsyncShouldReturnATaskConcerningThatRating()
        {
            var id = Guid.Parse("eeb15cc4-11c5-4897-a11d-2beca00abb81");

            var result = repository.GetByIdAsync(id);

            result.Should().BeOfType<Task<Rating>>();
        }

        [Test]
        public void Given_RatingId_When_RatingIdIsNull_Then_GetByIdAsyncShouldThrowArgumentException()
        {
            Guid id = Guid.Empty;

            Action result = () => repository.GetByIdAsync(id).Wait();

            result.Should().Throw<ArgumentException>();
        }

        [Test]
        public void Given_NewRating_When_RatingWasInDatabase_Then_UpdateAsyncShouldReturnATaskConcerningUpdatedRating()
        {
            var result = repository.UpdateAsync(newRating);

            result.Should().BeOfType<Task<Rating>>();
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
        }

        [Test]
        public void Given_Expression_When_RatingsPopulateDatabase_ThenQueryShouldReturnAQueryableCollectionOfRatingsRespectingThatExpression()
        {
            Expression<Func<Rating, bool>> expression = Rating => Rating.RatingValue > 8.9;

            var result = repository.Query(expression).ToList();

            result.Should().BeOfType<List<Rating>>();
        }
    }
}
