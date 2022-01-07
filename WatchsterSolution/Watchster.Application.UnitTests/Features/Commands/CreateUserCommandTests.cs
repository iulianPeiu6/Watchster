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
    public class CreateUserCommandTests
    {
        private readonly FakeUserRepository userRepository;
        private readonly ICryptographyService cryptography;
        private readonly CreateUserCommandHandler handler;

         public CreateUserCommandTests()
        {
            userRepository = A.Fake<FakeUserRepository>();
            cryptography = A.Fake<ICryptographyService>();
            handler = new CreateUserCommandHandler(userRepository, cryptography);
        }

        [Test]
        public async Task Given_CreateUserCommand_When_PasswordNotRespectConstrains_Should_FailedCreationAccountAsync()
        {
            //arrage
            var command = new CreateUserCommand
            {
                Email = Internet.Email(),
                IsSubscribed = (RandomNumber.Next() % 2) == 0,
                Password = ""
            };

            //act
            var response = await handler.Handle(command, default);

            //assert
            
            response.Should().NotBeNull();
            response.ErrorMessage.Should().Be(Error.InvalidData);
        }

        [Test]
        public async Task Given_CreateUserCommand_When_EmailIsInvalid_Should_FailedCreationAccountAsync()
        {
            //arrage
            var command = new CreateUserCommand
            {
                Email = "invalid email",
                IsSubscribed = (RandomNumber.Next() % 2) == 0,
                Password = "valid password"
            };

            //act
            var response = await handler.Handle(command, default);

            //assert

            response.Should().NotBeNull();
            response.ErrorMessage.Should().Be(Error.InvalidData);
        }

        [Test]
        public async Task Given_CreateUserCommand_When_ValidUserData_Should_SuccessCreateAccoutAsync()
        {
            //arrage
            var command = new CreateUserCommand
            {
                Email = Internet.Email(),
                IsSubscribed = (RandomNumber.Next() % 2) == 0,
                Password = Lorem.Sentence(6).Substring(0,31)
            };

            //act
            var response = await handler.Handle(command, default);

            //assert

            response.Should().NotBeNull();
            response.User.Id.Should().BeGreaterThan(-1);
            response.User.Email.Should().Be(command.Email);
            response.User.IsSubscribed.Should().Be(command.IsSubscribed);
            response.User.RegistrationDate.Should().BeBefore(DateTime.Now);
        }
    }
}
