using FakeItEasy;
using Faker;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using Watchster.Application.Features.Commands;
using Watchster.Application.Models;
using Watchster.Application.UnitTests.Fakes;
using Watchster.SendGrid.Services;

namespace Watchster.Application.UnitTests.Features.Commands
{
    public class SendResetMailCommandTests
    {

        private SendResetMailCommandHandler sendResetMailCommandHandler;

        public SendResetMailCommandTests()
        {
        }
        [Test]
        public async Task Given_SendResetMailCommand_When_InvalidEmail_Should_FailReturnEmailNotSend()
        {
            //arrage
            var command = new SendResetMailCommand()
            {
                Endpoint = Internet.Url(),
                Result = new Domain.Entities.ResetPasswordCode
                {
                    Email = "",
                    expirationDate = DateTime.Now,
                    Code = Faker.Identification.UsPassportNumber(),
                    Id = RandomNumber.Next(),

                }
            };
            sendResetMailCommandHandler = new SendResetMailCommandHandler(A.Fake<FakeSendGridService>());
            //act
            var response = await sendResetMailCommandHandler.Handle(command, default);

            //assert
            response.Should().NotBeNull();
            response.Should().Be(Error.EmailNotSent);
        }

        [Test]
        public async Task Given_SendResetMailCommand_When_ValidEmail_Should_ReturnSuccessString()
        {
            //arrage
            var command = new SendResetMailCommand()
            {
                Endpoint = Internet.Url(),
                Result = new Domain.Entities.ResetPasswordCode
                {
                    Email = Internet.Email(),
                    expirationDate = DateTime.Now,
                    Code = Faker.Identification.UsPassportNumber(),
                    Id = RandomNumber.Next(),

                }
            };
            sendResetMailCommandHandler = new SendResetMailCommandHandler(A.Fake<ISendGridService>());
            //act
            var response = await sendResetMailCommandHandler.Handle(command, default);

            //assert
            response.Should().NotBeNull();
            response.Should().Be("Email was sent");
        }
    }
}
