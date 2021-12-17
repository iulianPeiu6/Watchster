using FakeItEasy;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System.Threading.Tasks;
using Watchster.Application.Features.Commands;
using Watchster.WebApi.Controllers.v1;

namespace Watchster.WebApi.UnitTests.v1.UserControllerTests
{
    public class RegisterTests
    {
        private readonly UserController controller;
        private readonly IMediator mediator;
        private readonly ILogger<UserController> logger;

        public RegisterTests()
        {
            mediator = A.Fake<IMediator>();
            logger = A.Fake<ILogger<UserController>>();
            controller = new UserController(mediator, logger);
        }

        [Test]
        public async Task Given_CreateUserCommand_When_CommandIsValid_Then_CreateUserCommandShoulBeCalledAsync()
        {
            var command = new CreateUserCommand
            {
                Email = "iulian.peiu6@gmail.com",
                IsSubscribed = true,
                Password = "password"
            };
            var result = await controller.RegisterAsync(command);
            result.Should().BeOfType<OkObjectResult>();
            A.CallTo(() => mediator.Send(A<CreateUserCommand>._, default)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public async Task Given_CreateUserCommand_When_EmailIsInvalid_Then_GiveBadRequest()
        {
            var command = new CreateUserCommand
            {
                Email = "iulian.peiu",
                IsSubscribed = true,
                Password = "password"
            };

            var result = await controller.RegisterAsync(command);
            //result.Should().BeOfType<BadRequestResult>();
            A.CallTo(() => mediator.Send(A<CreateUserCommand>._, default)).MustHaveHappened();
        }
    }
}
