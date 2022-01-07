using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;
using Watchster.Application.Features.Queries;
using Watchster.Application.Interfaces;
using Watchster.Application.UnitTests.Fakes;

namespace Watchster.Application.UnitTests.Features.Queries
{
    public class GetRatingQueryTests
    {
        private readonly GetRatingQueryHandler handler;
        private readonly IRatingRepository repository;

        public GetRatingQueryTests()
        {
            repository = A.Fake<FakeRatingRepository>();
            handler = new GetRatingQueryHandler(repository);
        }

        [Test]
        public async Task Given_ValidGetRatingQuery_When_HandlerIsCalled_Then_ReturnRatingAsync()
        {
            // Arrange
            var query = new GetRatingQuery
            {
                MovieId = 1,
                UserId = 1
            };

            // Act
            var response = await handler.Handle(query, default);

            // Assert
            response.Should().Be(1);
        }

        [Test]
        public async Task Given_InvalidGetRatingQuery_When_HandlerIsCalled_Then_Return0Async()
        {
            // Arrange
            var query = new GetRatingQuery
            {
                MovieId = -1,
                UserId = -1
            };

            // Act
            var response = await handler.Handle(query, default);

            // Assert
            response.Should().Be(0);
        }
    }
}
