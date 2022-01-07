using FakeItEasy;
using Faker;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using Watchster.Application.Features.Commands;
using Watchster.Application.Models;
using Watchster.SendGrid.Models;
using Watchster.SendGrid.Services;

namespace Watchster.Application.UnitTests.Features.Commands
{
    public class SendResetMailCommandTests
    {

        private SendResetMailCommandHandler sendResetMailCommandHandler;
        private readonly ISendGridService sendGridService;
        private readonly string invalidEmail = "";

        public SendResetMailCommandTests()
        {
            var fakeSendGridService = new Fake<ISendGridService>();

            fakeSendGridService.CallsTo(sg => sg.SendMailAsync(A<MailInfo>.That.Matches(x => x.Receiver.Email == invalidEmail)))
                .Throws<ArgumentException>();
            sendGridService = fakeSendGridService.FakedObject;
            sendResetMailCommandHandler = new SendResetMailCommandHandler(sendGridService);
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

            //act
            var response = await sendResetMailCommandHandler.Handle(command, default);

            //assert
            response.Should().NotBeNull();
            response.Should().Be("Email was sent");
        }
    }
}
