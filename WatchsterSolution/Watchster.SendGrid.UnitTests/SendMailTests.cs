using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Watchster.SendGrid.Services;
using Microsoft.Extensions.Logging;
using Watchster.SendGrid.Models;
using SendGrid.Helpers.Mail;
using System;
using FakeItEasy;
using Microsoft.Extensions.Options;
using FluentAssertions;
using Randomizer.Types;
using SendGrid;

namespace Watchster.SendGrid.UnitTests
{
    public class SendMailTests
    {
        private readonly SendGridService sendGridService;

        public SendMailTests()
        {
            var logger = A.Fake<ILogger<SendGridService>>();
            //var config = A.Fake<IOptions<SendGridConfig>>();
            var configoptions = Options.Create(
                new SendGridConfig
                {
                    ApiKey = "entry string",
                    Sender = new EmailAddress
                    {
                        Email = "watchster.integration@gmail.com",
                        Name = "watchster"
                    }
                });
            this.sendGridService = new SendGridService(logger, configoptions);
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Given_Mail_When_MailIsNull_Then_SendMailAsyncShouldThrowAggregateExceptionContainingArgumentNullException()
        {
            Action action = () => sendGridService.SendMailAsync(null).Wait();
            action.Should().Throw<AggregateException>().And.InnerException.Should().BeOfType<ArgumentNullException>();
        }

        [Test]
        public void Given_Mail_When_ReceiverEmailIsNull_Then_SendMailAsyncShouldThrowAggregateExceptionContainingArgumentNullException()
        {
            RandomStringGenerator stringGeneretor = new RandomStringGenerator();
            var mail = new MailInfo
            {
                Subject = stringGeneretor.GenerateValue(),
                Body = stringGeneretor.GenerateValue(),
                Receiver = new EmailAddress()
                {
                    Name = stringGeneretor.GenerateValue(),
                    Email = null,
                }
            };
            Action action = () => sendGridService.SendMailAsync(mail).Wait();
            action.Should().Throw<AggregateException>().And.InnerException.Should().BeOfType<ArgumentNullException>();
        }

        //[Test]
        //public void Given_Mail_When_SenderEmailIsNull_SendEmailAsyncShouldBeCalledOnce()
        //{
        //    RandomStringGenerator stringGeneretor = new RandomStringGenerator();
        //    var mail = new MailInfo
        //    {
        //        Sender = null,
        //        Subject = stringGeneretor.GenerateValue(),
        //        Body = stringGeneretor.GenerateValue(),
        //        Receiver = new EmailAddress
        //        {
        //            Email = "watchster.integration@gmail.com",
        //            Name = stringGeneretor.GenerateValue()
        //        }
        //    };
        //    sendGridService.SendMailAsync(mail).Wait();
        //    var message = new SendGridMessage
        //    {
        //        From = mail.Sender,
        //        Subject = mail.Subject
        //    };
        //    message.AddContent(MimeType.Text, mail.Body);
        //    message.AddTo(mail.Receiver);
        //    A.CallTo(() => sendGridService.sendGridClient.SendEmailAsync(message, default)).MustHaveHappenedOnceExactly();
        //}

        [Test]
        public void Given_Mail_When_ReceiverEmailIsInvalid_Then_SendMailAsyncShouldThrowAggregateExceptionContainingArgumentException()
        {
            RandomStringGenerator stringGeneretor = new RandomStringGenerator();
            var mail = new MailInfo
            {
                Subject = stringGeneretor.GenerateValue(),
                Body = stringGeneretor.GenerateValue(),
                Receiver = new EmailAddress()
                {
                    Name = stringGeneretor.GenerateValue(),
                    Email = "",
                }
            };
            Action action = () => sendGridService.SendMailAsync(mail).Wait();
            action.Should().Throw<AggregateException>().And.InnerException.Should().BeOfType<ArgumentException>();
        }

        [Test]
        public void Given_Mail_When_SenderEmailIsInvalid_Then_SendMailAsyncShouldThrowAggregateExceptionContainingArgumentException()
        {
            RandomStringGenerator stringGeneretor = new RandomStringGenerator();
            var mail = new MailInfo
            {
                Sender = new EmailAddress
                {
                    Name = stringGeneretor.GenerateValue(),
                    Email = ""
                },
                Subject = stringGeneretor.GenerateValue(),
                Body = stringGeneretor.GenerateValue(),
                Receiver = new EmailAddress()
                {
                    Name = stringGeneretor.GenerateValue(),
                    Email = "watchster.integration@gmail.com",
                }
            };
            Action action = () => sendGridService.SendMailAsync(mail).Wait();
            action.Should().Throw<AggregateException>().And.InnerException.Should().BeOfType<ArgumentException>();
        }
    }
}
