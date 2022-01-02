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
    public class AuthenticateTests : UserControllerTestsBase
    {
        public AuthenticateTests() : base()
        {
        }

        [Test]
        public async Task Given_ValidAuthenticateUserCommand_When_AuthenticateIsCalled_Should_ReturnStatus200OkAsync()
        {
            //arrange
            var command = new AuthenticateUserCommand
            {
                Email = Internet.Email(),
                Password = Lorem.Sentence()
            };

            //act
            var response = await controller.AuthenticateAsync(command);

            //assert
            response.Should().BeOfType<OkObjectResult>();
            A.CallTo(() => mediator.Send(A<AuthenticateUserCommand>._, default)).MustHaveHappenedOnceExactly();
            response.Should().BeOfType<OkObjectResult>();
            response.As<OkObjectResult>().StatusCode.Should().Be(StatusCodes.Status200OK);
        }

        [Test]
        public async Task Given_InalidAuthenticateUserCommand_When_AuthenticateIsCalled_Should_ReturnStatus401UnauthorizedAsync()
        {
            //arrange
            var command = new AuthenticateUserCommand
            {
                Email = InvalidEmailAddress,
                Password = Lorem.Sentence()
            };

            //act
            var response = await controller.AuthenticateAsync(command);

            //assert
            response.Should().BeOfType<UnauthorizedObjectResult>();
            A.CallTo(() => mediator.Send(A<AuthenticateUserCommand>._, default)).MustHaveHappenedOnceExactly();
            response.Should().BeOfType<UnauthorizedObjectResult>();
            response.As<UnauthorizedObjectResult>().StatusCode.Should().Be(StatusCodes.Status401Unauthorized);
        }
    }
}
