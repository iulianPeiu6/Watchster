using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System.Threading.Tasks;
using Watchster.Application.Features.Commands;
using Watchster.WebApi.UnitTests.v1.Abstracts;

namespace Watchster.WebApi.UnitTests.v1.UserControllerTests
{
    public class VerifyPasswordCodeTests : UserControllerTestsBase
    {
        public VerifyPasswordCodeTests() : base()
        {
        }

        [Test]
        public async Task Given_ValidPasswordCode_When_VerifyPasswordCodeIsCalled_Should_ReturnStatus200OkAsync()
        {
            //arrange
            var command = new VerifyPasswordCodeCommand
            {
                Code = ValidPasswordCode
            };

            //act
            var response = await controller.VerifyPasswordCodeAsync(command);

            //assert
            A.CallTo(() => mediator.Send(A<VerifyPasswordCodeCommand>._, default)).MustHaveHappenedOnceExactly();
            response.Should().BeOfType<OkObjectResult>();
            response.As<OkObjectResult>().StatusCode.Should().Be(StatusCodes.Status200OK);
        }

        [Test]
        public async Task Given_InvalidPasswordCode_When_VerifyPasswordCodeIsCalled_Should_ReturnStatus401UnauthorizedAsync()
        {
            //arrange
            var command = new VerifyPasswordCodeCommand
            {
                Code = InvalidPasswordCode
            };

            //act
            var response = await controller.VerifyPasswordCodeAsync(command);

            //assert
            A.CallTo(() => mediator.Send(A<VerifyPasswordCodeCommand>._, default)).MustHaveHappenedOnceExactly();
            response.Should().BeOfType<UnauthorizedObjectResult>();
            response.As<UnauthorizedObjectResult>().StatusCode.Should().Be(StatusCodes.Status401Unauthorized);
        }
    }
}
