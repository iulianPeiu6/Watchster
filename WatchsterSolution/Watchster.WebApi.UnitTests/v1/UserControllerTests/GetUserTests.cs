using FakeItEasy;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System.Threading.Tasks;
using Watchster.Application.Features.Queries;
using Watchster.WebApi.Controllers.v1;

namespace Watchster.WebApi.UnitTests.v1.UserControllerTests
{
    public class GetUserTests
    {
        private readonly UserController controller;
        private readonly IMediator mediator;
        private readonly ILogger<UserController> logger;

        public GetUserTests()
        {
            mediator = A.Fake<IMediator>();
            logger = A.Fake<ILogger<UserController>>();
            controller = new UserController(mediator, logger);
        }

        [Test]
        public async Task Given_UserId_When_IdIsValid_Then_GetUserDetailsQueryHandlerIsCalledAsync()
        {
            var result = await controller.GetUser(-1);
            result.Should().BeOfType<OkObjectResult>();
            A.CallTo(() => mediator.Send(A<GetUserDetailsQuery>._, default)).MustHaveHappenedOnceExactly();
        }
    }
}
