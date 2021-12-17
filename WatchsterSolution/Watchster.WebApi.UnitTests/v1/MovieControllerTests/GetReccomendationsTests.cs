using FakeItEasy;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using Watchster.Application.Features.Queries;
using Watchster.WebApi.Controllers.v1;

namespace Watchster.WebApi.UnitTests.v1.MovieControllerTests
{
    public class GetReccomendationsTests
    {
        private readonly MovieController controller;
        private readonly IMediator mediator;

        public GetReccomendationsTests()
        {
            var logger = A.Fake<ILogger<MovieController>>();
            mediator = A.Fake<IMediator>();
            controller = new MovieController(mediator, logger);
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Given_MovieController_When_GetReccomendationsIsCalledWhileUserRatedAtLeastAMovie_Then_ShouldReturnOkResponse()
        {
            var result = controller.GetRecommendations(1).Result;
            result.Should().BeOfType<OkObjectResult>();
        }

        [Test]
        public void Given_MovieController_When_GetReccomendationsIsCalled_Then_QueryShouldHaveHappenedOnce()
        {
            var result = controller.GetRecommendations(1);
            A.CallTo(() => mediator.Send(A<GetReccomendationsQuery>._, default)).MustHaveHappenedOnceExactly();
        }
    }
}
