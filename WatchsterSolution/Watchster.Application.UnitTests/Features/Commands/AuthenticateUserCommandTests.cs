using FakeItEasy;
using Faker;
using FluentAssertions;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using System;
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
            var authConfig = new AuthenticationConfig()
            {
                Issuer = Name.FullName(),
                Key = Lorem.Sentence(),
                MinutesExpiration = 1440
            };
            config = Options.Create(authConfig);
            handler = new AuthenticateUserCommandHandler(userRepository, cryptography, config);
        }

        [Test]
        public async Task Given_AuthenticateUserCommand_When_WrongCredentials_Should_FailedAuthenticationAsync()
        {
            //arrage
            var command = new AuthenticateUserCommand()
            {
                Email = Internet.Email(),
                Password = Lorem.Sentence()
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
                Email = Internet.Email(),
                Password = Lorem.Sentence()
            };
            var user = await userRepository.AddAsync(new User
            {
                Id = 1,
                Email = command.Email,
                Password = cryptography.GetPasswordSHA3Hash(command.Password),
                IsSubscribed = true,
                RegistrationDate = DateTime.Now
            });

            //act
            var response = await handler.Handle(command, default);

            //assert
            A.CallTo(() => cryptography.GetPasswordSHA3Hash(command.Password)).MustHaveHappened();
            response.Should().NotBeNull();
            response.JwtToken.Should().NotBeNullOrEmpty();
            response.User.Should().NotBeNull();
            response.User.Id.Should().Be(1);
            response.User.Email.Should().Be(user.Email);
            response.User.IsSubscribed.Should().Be(user.IsSubscribed);
            response.User.RegistrationDate.Should().Be(user.RegistrationDate);
            response.ErrorMessage.Should().BeNullOrEmpty();
        }
    }
}
