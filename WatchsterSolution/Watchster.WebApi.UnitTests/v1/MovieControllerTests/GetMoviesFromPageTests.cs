using FakeItEasy;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using Watchster.Application.Features.Queries;
using Watchster.WebApi.Controllers.v1;

namespace Watchster.WebApi.UnitTests.GetMoviesFromPage
{
    public class GetMoviesFromPageTests
    {
        private readonly MovieController controller;
        private readonly IMediator mediator;

        public GetMoviesFromPageTests()
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
        public void Given_MovieController_When_GetMoviesFromPageIsCalledWithValidPageNumberCommand_Then_ShouldReturnOkResponse()
        {
            GetMoviesFromPageQuery command = new GetMoviesFromPageQuery
            {
                Page = 1
            };
            var result = controller.GetFromPage(command).Result;
            result.Should().BeOfType<OkObjectResult>();
        }
    }
}
