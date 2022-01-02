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
    public class ChangePasswordTests : UserControllerTestsBase
    {
        public ChangePasswordTests() : base()
        {
        }

        [Test]
        public async Task Given_ValidCode_When_ChangePasswordIsCalled_Should_ReturnStatus200OkAsync()
        {
            //arrange
            var command = new ChangeUserPasswordCommand
            {
                Code = ValidPasswordCode,
                Password = Lorem.Sentence()
            };
            Fake.ClearRecordedCalls(mediator);

            //act
            var response = await controller.ChangePasswordAsync(command);

            //assert
            A.CallTo(() => mediator.Send(A<ChangeUserPasswordCommand>._, default)).MustHaveHappenedOnceExactly();
            response.Should().BeOfType<OkObjectResult>();
            response.As<OkObjectResult>().StatusCode.Should().Be(StatusCodes.Status200OK);
        }

        [Test]
        public async Task Given_InvalidCode_When_ChangePasswordIsCalled_Should_ReturnStatus401UnauthorizedAsync()
        {
            //arrange
            var command = new ChangeUserPasswordCommand
            {
                Code = InvalidPasswordCode,
                Password = Lorem.Sentence()
            };
            Fake.ClearRecordedCalls(mediator);

            //act
            var response = await controller.ChangePasswordAsync(command);

            //assert
            A.CallTo(() => mediator.Send(A<ChangeUserPasswordCommand>._, default)).MustHaveHappenedOnceExactly();
            response.Should().BeOfType<UnauthorizedObjectResult>();
            response.As<UnauthorizedObjectResult>().StatusCode.Should().Be(StatusCodes.Status401Unauthorized);
        }
    }
}
