using FakeItEasy;
using Faker;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Watchster.Application.Features.Commands;
using Watchster.Application.Interfaces;
using Watchster.Application.Models;
using Watchster.Application.UnitTests.Fakes;

namespace Watchster.Application.UnitTests.Features.Commands
{
    public class GenerateAndSaveResetPasswordCodeCommandTests
    {
        private readonly FakeUserRepository userRepository;
        private readonly IResetPasswordCodeRepository resetPasswordCodeRepository;
        private readonly GenerateAndSaveResetPasswordCodeCommandHandler handler;

        public GenerateAndSaveResetPasswordCodeCommandTests()
        {
            userRepository = A.Fake<FakeUserRepository>();
            resetPasswordCodeRepository = A.Fake<IResetPasswordCodeRepository>();
            handler = new GenerateAndSaveResetPasswordCodeCommandHandler(userRepository, resetPasswordCodeRepository);
        }

        [Test]
        public async Task Given_GenerateAndSaveResetPasswordCodeCommand_When_UserNotFound_Should_FailedGenerateCodeAsync()
        {
            //arrage
            var command = new GenerateAndSaveResetPasswordCodeCommand
            {
                Email = Internet.Email(),
                Endpoint = Internet.Url()
            };

            //act
            var response = await handler.Handle(command, default);

            //assert

            response.Should().NotBeNull();
            response.ResetPasswordCode.Should().Be(null);
            response.ErrorMessage.Should().Be(Error.EmailNotFound);
        }

        [Test]
        public async Task Given_GenerateAndSaveResetPasswordCodeCommand_When_UserFound_Should_GenerateCodeAsync()
        {
            //arrage
            var command = new GenerateAndSaveResetPasswordCodeCommand
            {
                Email = "emai@emai.email",
                Endpoint = Internet.Url()
            };

            //act
            var response = await handler.Handle(command, default);

            //assert

            response.Should().NotBeNull();
            response.ResetPasswordCode.Should().NotBeNull();
            response.ResetPasswordCode.Code.Should().HaveLength(12);
            response.ResetPasswordCode.expirationDate.Should().NotBe(DateTime.Now);
            response.ResetPasswordCode.Email.Should().Be(command.Email);
        }
    }
}
