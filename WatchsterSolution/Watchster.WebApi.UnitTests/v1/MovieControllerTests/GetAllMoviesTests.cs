using FakeItEasy;
using MediatR;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using Watchster.Application.Features.Queries;
using Watchster.WebApi.Controllers.v1;

namespace Watchster.WebApi.UnitTests.v1.MovieControllerTests
{
    public class GetAllMoviesTests
    {
        private readonly MovieController controller;
        private readonly IMediator mediator;

        public GetAllMoviesTests()
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
        public void Given_MovieController_When_GetMoviesFromPageIsCalled_Then_QueryShouldHaveHappenedOnce()
        {
            var result = controller.GetAllAsync();
            A.CallTo(() => mediator.Send(A<GetAllMoviesQuery>._, default)).MustHaveHappenedOnceExactly();
        }
    }
}
