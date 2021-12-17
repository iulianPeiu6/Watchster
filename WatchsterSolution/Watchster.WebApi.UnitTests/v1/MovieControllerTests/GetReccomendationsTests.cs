using FakeItEasy;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System;
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
            controller = new MovieController(mediator, logger, null);
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Given_MovieController_When_GetReccomendationsIsCalledWhileUserRatedAtLeastAMovie_Then_ShouldReturnOkResponse()
        {
            var result = controller.GetRecommendations(Guid.Parse("8B36A8F5-0A04-4B4C-5107-08D9BB4D6F09")).Result;
            result.Should().BeOfType<OkObjectResult>();
        }

        //[Test]
        //public void Given_MovieController_When_GetReccomendationsIsCalledWhileUserRatedNoMovie_Then_ShouldReturnOkResponse()
        //{
        //    var result = controller.GetRecommendations(Guid.Parse("ffc19b3d-5e72-4c16-97a0-d4d5c74524d1")).Result;
        //    result.Should().BeOfType<BadRequestResult>();
        //}

        [Test]
        public void Given_MovieController_When_GetReccomendationsIsCalled_Then_QueryShouldHaveHappenedOnce()
        {
            var result = controller.GetRecommendations(Guid.Parse("8B36A8F5-0A04-4B4C-5107-08D9BB4D6F09"));
            A.CallTo(() => mediator.Send(A<GetReccomendationsQuery>._, default)).MustHaveHappenedOnceExactly();
        }
    }
}
