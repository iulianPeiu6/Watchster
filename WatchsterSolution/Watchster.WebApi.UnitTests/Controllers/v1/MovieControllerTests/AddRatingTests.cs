using FakeItEasy;
using Faker;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System.Threading.Tasks;
using Watchster.Application.Features.Commands;
using Watchster.WebApi.UnitTests.v1.Abstracts;

namespace Watchster.WebApi.UnitTests.v1.MovieControllerTests
{
    public class AddRatingTests : MovieControllerTestsBase
    {
        public AddRatingTests() : base()
        {
        }

        [Test]
        public async Task Given_ValidRating_When_AddRatingIsCalled_Should_ReturnStatus201CreatedResponseAsync()
        {
            //arrange
            var command = new AddRatingCommand
            {
                MovieId = RandomNumber.Next(1, int.MaxValue),
                UserId = RandomNumber.Next(1, int.MaxValue),
                Rating = RandomNumber.Next(1, 10)
            };

            //act
            var response = await controller.AddRatingAsync(command);

            //assert
            A.CallTo(() => mediator.Send(A<AddRatingCommand>._, default));
            response.Should().BeOfType<ObjectResult>();
            response.As<ObjectResult>().StatusCode.Should().Be(StatusCodes.Status201Created);
        }

        [Test]
        public async Task Given_RatingWithInvalidUserId_When_AddRatingIsCalled_Should_ReturnStatus404NotFoundResponseAsync()
        {
            //arrange
            var command = new AddRatingCommand
            {
                MovieId = RandomNumber.Next(1, int.MaxValue),
                UserId = -1,
                Rating = RandomNumber.Next(1, 10)
            };

            //act
            var response = await controller.AddRatingAsync(command);

            //assert
            A.CallTo(() => mediator.Send(A<AddRatingCommand>._, default));
            response.Should().BeOfType<NotFoundObjectResult>();
            response.As<NotFoundObjectResult>().StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }

        [Test]
        public async Task Given_RatingWithInvalidMovieId_When_AddRatingIsCalled_Should_ReturnStatus404NotFoundResponseAsync()
        {
            //arrange
            var command = new AddRatingCommand
            {
                MovieId = -1,
                UserId = RandomNumber.Next(1, int.MaxValue),
                Rating = RandomNumber.Next(1, 10)
            };

            //act
            var response = await controller.AddRatingAsync(command);

            //assert
            A.CallTo(() => mediator.Send(A<AddRatingCommand>._, default));
            response.Should().BeOfType<NotFoundObjectResult>();
            response.As<NotFoundObjectResult>().StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }

        [Test]
        public async Task Given_RatingWithInvalidRatingValueBiggerThan10_When_AddRatingIsCalled_Should_ReturnStatus404NotFoundResponseAsync()
        {
            //arrange
            var command = new AddRatingCommand
            {
                MovieId = RandomNumber.Next(1, int.MaxValue),
                UserId = RandomNumber.Next(1, int.MaxValue),
                Rating = 12.2
            };

            //act
            var response = await controller.AddRatingAsync(command);

            //assert
            A.CallTo(() => mediator.Send(A<AddRatingCommand>._, default));
            response.Should().BeOfType<BadRequestObjectResult>();
            response.As<BadRequestObjectResult>().StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        [Test]
        public async Task Given_RatingWithInvalidRatingValueLowerThan0_When_AddRatingIsCalled_Should_ReturnStatus400BadRequestResponseAsync()
        {
            //arrange
            var command = new AddRatingCommand
            {
                MovieId = RandomNumber.Next(1, int.MaxValue),
                UserId = RandomNumber.Next(1, int.MaxValue),
                Rating = -2.2
            };

            //act
            var response = await controller.AddRatingAsync(command);

            //assert
            A.CallTo(() => mediator.Send(A<AddRatingCommand>._, default));
            response.Should().BeOfType<BadRequestObjectResult>();
            response.As<BadRequestObjectResult>().StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        [Test]
        public async Task Given_InvalidRatingForMovieAlreadyRatedByUser_When_AddRatingIsCalled_Should_ReturnStatus406NotAcceptableResponseAsync()
        {
            //arrange
            var command = new AddRatingCommand
            {
                MovieId = 0,
                UserId = 0,
                Rating = RandomNumber.Next(1, 10)
            };

            //act
            var response = await controller.AddRatingAsync(command);

            //assert
            A.CallTo(() => mediator.Send(A<AddRatingCommand>._, default));
            response.Should().BeOfType<ObjectResult>();
            response.As<ObjectResult>().StatusCode.Should().Be(StatusCodes.Status406NotAcceptable);
        }
    }
}
