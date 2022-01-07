using FakeItEasy;
using Faker;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;
using Watchster.Application.Features.Commands;
using Watchster.Application.Interfaces;
using Watchster.Application.Models;
using Watchster.Application.UnitTests.Fakes;

namespace Watchster.Application.UnitTests.Features.Commands
{
    public class ChangeUserPasswordCommandTest
    {
        private const int minValue = 1;
        private const int maxValue = int.MaxValue;
        private FakeUserRepository userRepository;
        private FakeResetPasswordCodeRepository resetPasswordCodeRepository;
        private ICryptographyService cryptography;
        private ChangeUserPasswordCommandHandler changeUserCommandHandler;

        public ChangeUserPasswordCommandTest()
        {
            userRepository = A.Fake<FakeUserRepository>();
            resetPasswordCodeRepository = A.Fake<FakeResetPasswordCodeRepository>();
            cryptography = A.Fake<ICryptographyService>();
            changeUserCommandHandler = new ChangeUserPasswordCommandHandler(userRepository, resetPasswordCodeRepository, cryptography);
        }

        [Test]
        public async Task Given_ChangeUserPasswordCommand_When_UserCodeNotFound_Should_FailReturnChangePasswordResult()
        {
            //arrage
            var command = new ChangeUserPasswordCommand()
            {
                Code = "",
                Password = Lorem.Sentence(),
            };

            //act
            var response = await changeUserCommandHandler.Handle(command, default);

            //assert
            response.Should().NotBeNull();
            response.ErrorMessage.Should().Be(Error.WrongPassChangeCode);
            response.IsSuccess.Should().Be(false);
        }

        [Test]
        public async Task Given_ChangeUserPasswordCommand_When_UserCodeFoundButUserNotFound_Should_FailReturnChangePasswordResult()
        {
            //arrage
            var command = new ChangeUserPasswordCommand()
            {
                Code = "code without email",
            };

            //act
            var response = await changeUserCommandHandler.Handle(command, default);

            //assert
            response.Should().NotBeNull();
            response.ErrorMessage.Should().Be(Error.WrongPassChangeCode);
            response.IsSuccess.Should().Be(false);
        }

        [Test]
        public async Task Given_ChangeUserPasswordCommand_When_UserCodeFoundButUserFoundPasswordNotRespectingContrains_Should_FailReturnChangePasswordResult()
        {
            //arrage
            var command = new ChangeUserPasswordCommand()
            {
                Code = "hardcoded",
                Password = ""
            };

            //act
            var response = await changeUserCommandHandler.Handle(command, default);

            //assert
            response.Should().NotBeNull();
            response.ErrorMessage.Should().Be(Error.InvalidPass);
            response.IsSuccess.Should().Be(false);
        }

        [Test]
        public async Task Given_ChangeUserPasswordCommand_When_UserCodeFoundButUserFoundPasswordRespectingContrains_Should_ChangeUserPassword()
        {
            //arrage
            var command = new ChangeUserPasswordCommand()
            {
                Code = "hardcoded",
                Password = "123456"
            };

            //act
            var response = await changeUserCommandHandler.Handle(command, default);

            //assert
            response.Should().NotBeNull();
            response.ErrorMessage.Should().Be(null);
            response.IsSuccess.Should().Be(true);
            A.CallTo(() => cryptography.GetPasswordSHA3Hash(command.Password)).MustHaveHappened();
        }
    }
}
