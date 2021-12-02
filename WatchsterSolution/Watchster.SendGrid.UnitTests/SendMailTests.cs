using FakeItEasy;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using Watchster.SendGrid.Models;
using Watchster.SendGrid.Services;

namespace Watchster.SendGrid.UnitTests
{
    public class SendMailTests
    {
        private readonly SendGridService sendGridService;
        private readonly SendGridClient sendGridClient;

        public SendMailTests()
        {
            var logger = A.Fake<ILogger<SendGridService>>();
            //var config = A.Fake<IOptions<SendGridConfig>>();
            var config = Options.Create(
                new SendGridConfig
                {
                    ApiKey = Faker.Lorem.Sentence(),
                    Sender = new EmailAddress
                    {
                        Email = Faker.Internet.Email(),
                        Name = Faker.Name.FullName()
                    }
                });
            //sendGridClient = A.Fake<SendGridClient>();
            sendGridClient = new SendGridClient(config.Value.ApiKey);

            sendGridService = new SendGridService(logger, config, sendGridClient);
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Given_Mail_When_MailIsNull_Then_SendMailAsyncShouldThrowAggregateExceptionContainingArgumentNullException()
        {
            MailInfo mail = null;

            Action action = () => sendGridService.SendMailAsync(mail).Wait();

            action.Should().Throw<AggregateException>().And.InnerException.Should().BeOfType<ArgumentNullException>();
        }

        [Test]
        public void Given_Mail_When_ReceiverEmailIsNull_Then_SendMailAsyncShouldThrowArgumentNullException()
        {
            var mail = new MailInfo
            {
                Subject = string.Join(" ", Faker.Lorem.Sentences(3)),
                Body = string.Join(" ", Faker.Lorem.Sentences(3)),
                Receiver = new EmailAddress()
                {
                    Name = Faker.Name.FullName(),
                    Email = null,
                }
            };

            Action action = () => sendGridService.SendMailAsync(mail).Wait();

            action.Should().Throw<AggregateException>().And.InnerException.Should().BeOfType<ArgumentNullException>();
        }

        [Test]
        public void Given_Mail_When_ReceiverEmailIsEmpty_Then_SendMailAsyncShouldThrowArgumentException()
        {
            var mail = new MailInfo
            {
                Subject = string.Join(" ", Faker.Lorem.Sentences(3)),
                Body = string.Join(" ", Faker.Lorem.Sentences(3)),
                Receiver = new EmailAddress()
                {
                    Name = Faker.Name.FullName(),
                    Email = "",
                }
            };

            Action action = () => sendGridService.SendMailAsync(mail).Wait();

            action.Should().Throw<AggregateException>().And.InnerException.Should().BeOfType<ArgumentException>();
        }

        [Test]
        public void Given_Mail_When_SenderEmailIsInvalid_Then_SendMailAsyncShouldThrowArgumentException()
        {
            var mail = new MailInfo
            {
                Sender = new EmailAddress
                {
                    Name = Faker.Name.FullName(),
                    Email = ""
                },
                Subject = string.Join(" ", Faker.Lorem.Sentences(3)),
                Body = string.Join(" ", Faker.Lorem.Sentences(3)),
                Receiver = new EmailAddress()
                {
                    Name = Faker.Name.FullName(),
                    Email = Faker.Internet.Email()
                }
            };

            Action action = () => sendGridService.SendMailAsync(mail).Wait();

            action.Should().Throw<AggregateException>().And.InnerException.Should().BeOfType<ArgumentException>();
        }
    }
}
