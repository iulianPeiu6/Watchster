using FakeItEasy;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using shortid;
using shortid.Configuration;
using System;
using System.Threading.Tasks;
using Watchster.Application.Features.Commands;
using Watchster.WebApi.Controllers.v1;

namespace Watchster.WebApi.UnitTests.v1.UserControllerTests
{
    public class PasswordResetTests
    {
        private readonly UserController controller;
        private readonly IMediator mediator;
        private readonly ILogger<UserController> logger;

        public PasswordResetTests()
        {
            mediator = A.Fake<IMediator>();
            logger = A.Fake<ILogger<UserController>>();
            controller = new UserController(mediator, logger);
        }

        [Test]
        public void Given_Email_When_EmailIsValid_Then_SendEmailChangePasswordAsyncShouldNotThrowException()
        {
            GenerateAndSaveResetPasswordIDCommand command = new GenerateAndSaveResetPasswordIDCommand
            {
                Email = Faker.Internet.Email(),
                Endpoint = Faker.Internet.DomainName()
            };
            Func<Task> action = async () => await controller.SendEmailChangePasswordAsync(command);

            action.Should().NotThrowAsync();
        }

        [Test]
        public void Given_Email_When_EmailIsInValid_Then_SendEmailChangePasswordAsyncShouldThrowAggregateException()
        {
            GenerateAndSaveResetPasswordIDCommand command = new GenerateAndSaveResetPasswordIDCommand
            {
                Email = "invalid",
                Endpoint = Faker.Internet.DomainName()
            };
            Func<Task> action = async () => await controller.SendEmailChangePasswordAsync(command);

            action.Should().ThrowAsync<AggregateException>();
        }

        [Test]
        public void Given_Code_When_CodeIsNotInDatabase_Then_VerifyPasswordCodeAsyncShouldThrowAggregateException()
        {
            var options = new GenerationOptions
            {
                Length = 12
            };

            VerifyPasswordCodeCommand command = new VerifyPasswordCodeCommand
            {
                Code = ShortId.Generate(options)
            };

            Func<Task> action = async () => await controller.VerifyPasswordCodeAsync(command);

            action.Should().ThrowAsync<AggregateException>();
        }
    }
}