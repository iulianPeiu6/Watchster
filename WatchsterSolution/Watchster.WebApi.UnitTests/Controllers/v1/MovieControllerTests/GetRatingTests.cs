using FakeItEasy;
using Faker;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System.Threading.Tasks;
using Watchster.Application.Features.Queries;
using Watchster.WebApi.UnitTests.v1.Abstracts;

namespace Watchster.WebApi.UnitTests.Controllers.v1.MovieControllerTests
{
    public class GetRatingTests : MovieControllerTestsBase
    {
        public GetRatingTests() : base()
        {
        }

        [Test]
        public async Task Given_GetRatingQuery_When_GetRatingAsyncIsCalled_Should_ReturnStatus200OkResponseAsync()
        {
            //arrange
            var query = new GetRatingQuery
            {
                MovieId = RandomNumber.Next(1, int.MaxValue),
                UserId = RandomNumber.Next(1, int.MaxValue)
            };

            //act
            var response = await controller.GetRatingAsync(query);

            //assert
            A.CallTo(() => mediator.Send(A<GetRatingQuery>._, default));
            response.Should().BeOfType<OkObjectResult>();
            response.As<OkObjectResult>().StatusCode.Should().Be(StatusCodes.Status200OK);
        }
    }
}
