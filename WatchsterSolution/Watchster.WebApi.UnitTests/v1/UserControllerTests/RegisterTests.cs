using FakeItEasy;
using Faker;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System.Threading.Tasks;
using Watchster.Application.Features.Commands;
using Watchster.WebApi.UnitTests.v1.Abstracts;

namespace Watchster.WebApi.UnitTests.v1.UserControllerTests
{
    public class RegisterTests : UserControllerTestsBase
    {
        public RegisterTests() : base()
        {
        }

        [Test]
        public async Task Given_CreateUserCommand_When_CommandIsValid_Should_ReturnStatus200OkAsync()
        {
            //arrange
            var command = new CreateUserCommand
            {
                Email = Internet.Email(),
                IsSubscribed = true,
                Password = Lorem.Sentence()
            };

            //act
            var response = await controller.RegisterAsync(command);

            //assert
            A.CallTo(() => mediator.Send(A<CreateUserCommand>._, default)).MustHaveHappenedOnceExactly();
            response.Should().BeOfType<OkObjectResult>();
            response.As<OkObjectResult>().StatusCode.Should().Be(StatusCodes.Status200OK);
        }

        [Test]
        public async Task Given_CreateUserCommand_When_CommandIsInvalid_Should_ReturnStatus400BadRequestAsync()
        {
            //arrange
            var command = new CreateUserCommand
            {
                Email = InvalidEmailAddress,
                IsSubscribed = true,
                Password = Lorem.Sentence()
            };

            //act
            var response = await controller.RegisterAsync(command);

            //assert
            A.CallTo(() => mediator.Send(A<CreateUserCommand>._, default)).MustHaveHappened();
            response.Should().BeOfType<BadRequestResult>();
            response.As<BadRequestResult>().StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }
    }
}
