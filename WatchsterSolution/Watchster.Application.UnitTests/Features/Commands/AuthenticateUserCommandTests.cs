using FakeItEasy;
using FluentAssertions;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using System.Threading.Tasks;
using Watchster.Application.Features.Commands;
using Watchster.Application.Features.Queries;
using Watchster.Application.Interfaces;
using Watchster.Application.Models;
using Watchster.Application.UnitTests.Fakes;
using Watchster.Application.Utils.Cryptography;
using Watchster.Domain.Entities;
using Watchster.Jwt;

namespace Watchster.Application.UnitTests.Features.Commands
{
    public class AuthenticateUserCommandTests
    {
        private readonly FakeUserRepository userRepository;
        private readonly ICryptographyService cryptography;
        private readonly IOptions<AuthenticationConfig> config;
        private readonly AuthenticateUserCommandHandler handler;

        public AuthenticateUserCommandTests()
        {
            userRepository = A.Fake<FakeUserRepository>();
            cryptography = A.Fake<ICryptographyService>();
            config = Options.Create(
                new AuthenticationConfig()
                {
                    Issuer = "Watchster",
                    Key = "this is my test secret key for authnetication",
                    MinutesExpiration = 1440
                });
            handler = new AuthenticateUserCommandHandler(userRepository, cryptography, config);
        }

        [Test]
        public async Task Given_AuthenticateUserCommand_When_WrongCredentials_Should_FailedAuthenticationAsync()
        {
            //arrage
            var command = new AuthenticateUserCommand()
            {
                Email = "wrongtest@email.test",
                Password = "wrong password"
            };

            //act
            var response = await handler.Handle(command, default);

            //assert
            A.CallTo(() => cryptography.GetPasswordSHA3Hash(command.Password)).MustHaveHappened();
            response.Should().NotBeNull();
            response.JwtToken.Should().BeNullOrEmpty();
            response.User.Should().BeNull();
            response.ErrorMessage.Should().Be(Error.WrongEmailOrPass);
        }

        [Test]
        public async Task Given_AuthenticateUserCommand_When_CredentialsMatches_Should_AuthenticateSuccessfullyAsync()
        {
            //arrage
            var command = new AuthenticateUserCommand()
            {
                Email = "test@email.test",
                Password = "password"
            };
            var user = await userRepository.AddAsync(new User
            {
                Email = command.Email,
                Password = cryptography.GetPasswordSHA3Hash(command.Password)
            });

            //act
            var response = await handler.Handle(command, default);

            //assert
            A.CallTo(() => cryptography.GetPasswordSHA3Hash(command.Password)).MustHaveHappened();
            response.Should().NotBeNull();
            response.JwtToken.Should().NotBeNullOrEmpty();
            response.User.Should().NotBeNull();
            response.User.Email.Should().Be(user.Email);
            response.ErrorMessage.Should().BeNullOrEmpty();
        }
    }
}
