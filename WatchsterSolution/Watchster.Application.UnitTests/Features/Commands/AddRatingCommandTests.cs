using FakeItEasy;
using Faker;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;
using Watchster.Application.Features.Commands;
using Watchster.Application.Models;
using Watchster.Application.UnitTests.Fakes;

namespace Watchster.Application.UnitTests.Features.Commands
{

    public class AddRatingCommandTests
    {
        private const int minValue = 1;
        private const int maxValue = int.MaxValue;
        private FakeUserRepository userRepository;
        private readonly FakeMovieRepository fakeMovieRepository;
        private readonly FakeRatingRepository fakeRatingRepository;
        private readonly AddRatingCommandHandler handler;

        public AddRatingCommandTests()
        {
            userRepository = A.Fake<FakeUserRepository>();
            fakeMovieRepository = A.Fake<FakeMovieRepository>();
            fakeRatingRepository = A.Fake<FakeRatingRepository>();
            handler = new AddRatingCommandHandler(fakeMovieRepository, fakeRatingRepository, userRepository);
        }

        [Test]
        public async Task Given_AddRatingCommand_When_InvalidUserId_Should_FailReturnAddRatingResponse()
        {
            //arrage
            var command = new AddRatingCommand()
            {
                UserId = -1,
                MovieId = RandomNumber.Next(minValue, maxValue),
                Rating = RandomNumber.Next(minValue, maxValue) % 5,
            };

            //act
            var response = await handler.Handle(command, default);

            //assert
            response.Should().NotBeNull();
            response.ErrorMessage.Should().Be(Error.UserNotFound);
            response.IsSuccess.Should().Be(false);
        }

        [Test]
        public async Task Given_AddRatingCommand_When_InvalidMovieId_Should_FailReturnAddRatingResponse()
        {
            //arrage
            var command = new AddRatingCommand()
            {
                UserId = RandomNumber.Next(minValue, maxValue),
                MovieId = -1,
                Rating = RandomNumber.Next(minValue, maxValue) % 5,
            };

            //act
            var response = await handler.Handle(command, default);

            //assert
            response.Should().NotBeNull();
            response.ErrorMessage.Should().Be(Error.MovieNotFound);
            response.IsSuccess.Should().Be(false);
        }

        [Test]
        public async Task Given_AddRatingCommand_When_ExistingRating_Should_FailReturnAddRatingResponse()
        {
            //arrage
            var command = new AddRatingCommand()
            {
                UserId = 1,
                MovieId = 1,
                Rating = 1,
            };

            //act
            var response = await handler.Handle(command, default);

            //assert
            response.Should().NotBeNull();
            response.ErrorMessage.Should().Be(Error.MovieAlreadyRated);
            response.IsSuccess.Should().Be(false);
        }

        [Test]
        public async Task Given_AddRatingCommand_When_NotExistingRatingButOutOfRangeRating_Should_FailReturnAddRatingResponse()
        {
            //arrage
            var command = new AddRatingCommand()
            {
                UserId = 1,
                MovieId = 2,
                Rating = -1,
            };

            //act
            var response = await handler.Handle(command, default);

            //assert
            response.Should().NotBeNull();
            response.ErrorMessage.Should().Be(Error.RatingNotInRange);
            response.IsSuccess.Should().Be(false);
        }

        [Test]
        public async Task Given_AddRatingCommand_When_ValidRating_Should_AddRating()
        {
            //arrage
            var command = new AddRatingCommand()
            {
                UserId = 1,
                MovieId = 2,
                Rating = 5,
            };

            //act
            var response = await handler.Handle(command, default);

            //assert
            response.Should().NotBeNull();
            response.IsSuccess.Should().Be(true);
        }

    }
}
