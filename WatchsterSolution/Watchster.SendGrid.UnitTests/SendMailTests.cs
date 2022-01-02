using FakeItEasy;
using Faker;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Net;
using System.Threading.Tasks;
using Watchster.SendGrid.Models;
using Watchster.SendGrid.Services;

namespace Watchster.SendGrid.UnitTests
{
    public class SendMailTests
    {
        private readonly SendGridService sendGridService;
        private readonly SendGridService sendGrid;
        private readonly ISendGridClient sendGridClient;
        private const string InvalidEmail = "invalid email";

        public SendMailTests()
        {
            var logger = A.Fake<ILogger<SendGridService>>();
            var config = Options.Create(
                new SendGridConfig
                {
                    ApiKey = Lorem.Sentence(),
                    Sender = new EmailAddress
                    {
                        Email = Internet.Email(),
                        Name = Name.FullName()
                    }
                });
            var fakeSendGridClient = new Fake<ISendGridClient>();

            fakeSendGridClient.CallsTo(sg => sg.SendEmailAsync(A<SendGridMessage>._, default))
                .Returns(Task.FromResult(new Response(HttpStatusCode.OK, null, null)));

            sendGridClient = fakeSendGridClient.FakedObject;

            sendGridService = new SendGridService(logger, config, sendGridClient);
            sendGrid = new SendGridService(logger, config);
        }

        [SetUp]
        public void Setup()
        {
            Fake.ClearRecordedCalls(sendGridClient);
        }

        [Test]
        public void Given_ContructorArguments_When_ContructorIsCalled__Should_CreateObject()
        {
            //arrange
            MailInfo mail = null;

            //act
            Action action = () => sendGrid.SendMailAsync(mail).Wait();

            //assert
            action.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Given_NullMail_When_SendMailAsyncIsCalled_Should_ThrowArgumentNullException()
        {
            //arrange
            MailInfo mail = null;

            //act
            Action action = () => sendGridService.SendMailAsync(mail).Wait();

            //assert
            action.Should().Throw<ArgumentNullException>();
            A.CallTo(() => sendGridClient.SendEmailAsync(A<SendGridMessage>._, default)).MustNotHaveHappened();
        }

        [Test]
        public void Given_MailWithNullReceiverEmail_When_SendMailAsyncIsCalled_Should_ThrowArgumentNullException()
        {
            //arrange
            var mail = new MailInfo
            {
                Subject = string.Join(" ", Lorem.Sentences(3)),
                Body = string.Join(" ", Lorem.Sentences(3)),
                Receiver = new EmailAddress()
                {
                    Name = Name.FullName(),
                    Email = null,
                },
                Sender = new EmailAddress()
                {
                    Name = Name.FullName(),
                    Email = Internet.Email(),
                }
            };

            //act
            Action action = () => sendGridService.SendMailAsync(mail).Wait();

            //assert
            action.Should().Throw<ArgumentNullException>();
            A.CallTo(() => sendGridClient.SendEmailAsync(A<SendGridMessage>._, default)).MustNotHaveHappened();
        }

        [Test]
        public void Given_MailWithInvalidReceiverEmail_When_SendMailAsyncIsCalled_Should_ThrowArgumentExceptionAsync()
        {
            //arrange
            var mail = new MailInfo
            {
                Subject = string.Join(" ", Lorem.Sentences(3)),
                Body = string.Join(" ", Lorem.Sentences(3)),
                Receiver = new EmailAddress()
                {
                    Name = Name.FullName(),
                    Email = InvalidEmail,
                },
                Sender = new EmailAddress()
                {
                    Name = Name.FullName(),
                    Email = Internet.Email(),
                }
            };

            //act
            Action action = () => sendGridService.SendMailAsync(mail).Wait();

            //assert
            action.Should().Throw<ArgumentException>().WithMessage("Receiver Email Address is Invalid");
            A.CallTo(() => sendGridClient.SendEmailAsync(A<SendGridMessage>._, default)).MustNotHaveHappened();
        }

        [Test]
        public void Given_MailWithInvalidSenderEmail_When_SendMailAsyncIsCalled_Should_ThrowArgumentExceptionAsync()
        {
            //arrange
            var mail = new MailInfo
            {
                Subject = string.Join(" ", Lorem.Sentences(3)),
                Body = string.Join(" ", Lorem.Sentences(3)),
                Receiver = new EmailAddress()
                {
                    Name = Name.FullName(),
                    Email = Internet.Email(),
                },
                Sender = new EmailAddress()
                {
                    Name = Name.FullName(),
                    Email = InvalidEmail,
                }
            };

            //act
            Action action = () => sendGridService.SendMailAsync(mail).Wait();

            //assert
            action.Should().Throw<ArgumentException>().WithMessage("Sender Email Address is Invalid");
            A.CallTo(() => sendGridClient.SendEmailAsync(A<SendGridMessage>._, default)).MustNotHaveHappened();
        }

        [Test]
        public async Task Given_MailWithNullSenderEmail_When_SendMailAsyncIsCalled_Should_UseTheOneFromConfigAsync()
        {
            //arrange
            var mail = new MailInfo
            {
                Subject = string.Join(" ", Lorem.Sentences(3)),
                Body = string.Join(" ", Lorem.Sentences(3)),
                Receiver = new EmailAddress()
                {
                    Name = Name.FullName(),
                    Email = Internet.Email(),
                }
            };

            //act
            await sendGridService.SendMailAsync(mail);

            //arrange
            A.CallTo(() => sendGridClient.SendEmailAsync(A<SendGridMessage>._, default)).MustHaveHappenedOnceExactly();
        }
    }
}
