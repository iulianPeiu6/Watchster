using MediatR;
using Watchster.WebApi.Controllers.v1;
using FakeItEasy;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using Watchster.Application.Features.Queries;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace Watchster.WebApi.UnitTests.GetMoviesFromPage
{
    public class MovieControllerTests
    {
        private readonly MovieController controller;
        private readonly IMediator mediator;

        public MovieControllerTests()
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
        public void Given_MovieController_When_GetMoviesFromPageIsCalledWithValidPageNumberCommand_Then_ShouldReturnOkResponse()
        {
            GetMoviesFromPageCommand command = new GetMoviesFromPageCommand
            {
                Page = 1
            };
            var result = controller.GetFromPage(command).Result;
            result.Should().BeOfType<OkObjectResult>();
        }
    }
}
