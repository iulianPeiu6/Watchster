using FakeItEasy;
using Faker;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System.Threading.Tasks;
using Watchster.Application.Features.Queries;
using Watchster.WebApi.UnitTests.v1.Abstracts;

namespace Watchster.WebApi.UnitTests.v1.MovieControllerTests
{
    public class GetMovieTests : MovieControllerTestsBase
    {
        public GetMovieTests() : base()
        {
        }

        [Test]
        public async Task Given_MovieValidId_When_GetMovie_Should_ReturnOkResponseWithMovieAsync()
        {
            //arrange
            var movieId = RandomNumber.Next(1, int.MaxValue);

            //act
            var response = await controller.GetMovieAsync(movieId);

            //assert
            A.CallTo(() => mediator.Send(A<GetMovieByIdQuery>._, default));
            response.Should().BeOfType<OkObjectResult>();
        }

        [Test]
        public async Task Given_MovieInValidId_When_GetMovie_Should_ReturnNotFoundResponseAsync()
        {
            //arange
            var movieId = InvalidMovieId;

            //act
            var response = await controller.GetMovieAsync(movieId);

            //assert
            A.CallTo(() => mediator.Send(A<GetMovieByIdQuery>._, default));
            response.Should().BeOfType<NotFoundObjectResult>();
            response.As<NotFoundObjectResult>().StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }
    }
}
