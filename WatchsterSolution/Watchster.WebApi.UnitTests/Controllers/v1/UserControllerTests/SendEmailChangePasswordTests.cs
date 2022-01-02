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
    public class SendEmailChangePasswordTests : UserControllerTestsBase
    {
        public SendEmailChangePasswordTests() : base()
        {
        }

        [Test]
        public async Task Given_ValidArguments_When_SendEmailChangePasswordIsCalled_Should_ReturnStatus200OkAsync()
        {
            //arrange
            var command = new GenerateAndSaveResetPasswordCodeCommand
            {
                Email = Faker.Internet.Email(),
                Endpoint = Faker.Internet.DomainName()
            };
            Fake.ClearRecordedCalls(mediator);

            //act
            var response = await controller.SendEmailChangePasswordAsync(command);

            //assert
            A.CallTo(() => mediator.Send(A<GenerateAndSaveResetPasswordCodeCommand>._, default)).MustHaveHappenedOnceExactly();
            A.CallTo(() => mediator.Send(A<SendResetMailCommand>._, default)).MustHaveHappenedOnceExactly();
            response.Should().BeOfType<OkObjectResult>();
            response.As<OkObjectResult>().StatusCode.Should().Be(StatusCodes.Status200OK);
        }

        [Test]
        public async Task Given_ArgumentsWithInvalidEmail_When_SendEmailChangePasswordIsCalled_Should_ReturnStatus404NotFoundAsync()
        {
            //arrange
            var command = new GenerateAndSaveResetPasswordCodeCommand
            {
                Email = InvalidEmailAddress,
                Endpoint = Faker.Internet.DomainName()
            };
            Fake.ClearRecordedCalls(mediator);

            //act
            var response = await controller.SendEmailChangePasswordAsync(command);

            //assert
            A.CallTo(() => mediator.Send(A<GenerateAndSaveResetPasswordCodeCommand>._, default)).MustHaveHappenedOnceExactly();
            A.CallTo(() => mediator.Send(A<SendResetMailCommand>._, default)).MustNotHaveHappened();
            response.Should().BeOfType<NotFoundObjectResult>();
            response.As<NotFoundObjectResult>().StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }

        [Test]
        public async Task Given_ArgumentsWithUnreachbleEmail_When_SendEmailChangePasswordIsCalled_Should_ReturnStatus404NotFoundAsync()
        {
            //arrange
            var command = new GenerateAndSaveResetPasswordCodeCommand
            {
                Email = UnreachableEmailAddress,
                Endpoint = Faker.Internet.DomainName()
            };
            Fake.ClearRecordedCalls(mediator);

            //act
            var response = await controller.SendEmailChangePasswordAsync(command);

            //assert
            A.CallTo(() => mediator.Send(A<GenerateAndSaveResetPasswordCodeCommand>._, default)).MustHaveHappenedOnceExactly();
            A.CallTo(() => mediator.Send(A<SendResetMailCommand>._, default)).MustHaveHappenedOnceExactly();
            response.Should().BeOfType<ObjectResult>();
            response.As<ObjectResult>().StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
        }
    }
}