using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Watchster.SendGrid.Services;
using Microsoft.Extensions.Logging;
using Watchster.SendGrid.Models;
using SendGrid.Helpers.Mail;
using System;

namespace Watchster.SendGrid.UnitTests
{
    public class Tests
    {
        ISendGridService _sendGridService;
        [SetUp]
        public void Setup()
        {


            var services = new ServiceCollection()
               .AddSendGrid()
               .AddLogging()
               .Configure<LoggerFilterOptions>(options => options.MinLevel = LogLevel.Information)
               .BuildServiceProvider();

            var sendGridService = services.GetRequiredService<ISendGridService>();
            _sendGridService = sendGridService;
        }

        [Test]
        public void Given_Mail_WhenMailIsNull_Then_SendMailAsyncShouldThrowException()
        {
            try
            {
                _sendGridService.SendMailAsync(null).Wait();
            }
            catch (Exception e)
            {
                Assert.Pass();
            }
            Assert.Fail();
        }

        [Test]
        public void Given_Mail_WhereReceiverAddressHasRightFormatButFalseAdresss_Then_SendMailAsyncShouldSendEmail()
        {
            var mail = new MailInfo
            {
                Subject = "Subject",
                Body = "Body text",
                Receiver = new EmailAddress()
                {
                    Name = "Name",
                    Email = "falseadress@adress.adress"
                }
            };
            try
            {
                _sendGridService.SendMailAsync(mail).Wait();
            }
            catch (Exception e)
            {
                Assert.Fail();

            }
            Assert.Pass();
        }
        
        [Test]
        public void Given_Mail_WhereReceiverAddressHasWrongFormat_Then_SendMailAsyncShouldThrowException()
        {
            var mail = new MailInfo
            {
                Subject = "Subject",
                Body = "Body text",
                Receiver = new EmailAddress()
                {
                    Name = "Receiver",
                    Email = "wrong-format"
                }
            };
            try
            {
                _sendGridService.SendMailAsync(mail).Wait();
            }
            catch (Exception e)
            {
                Assert.Pass();
            }
            Assert.Fail();
        }
        [Test]
        public void Given_Mail_WhereSenderAddressIsChangedToValidAdressButKeyIsInvalid_Then_SendMailAsyncShouldThrowException()
        {
            var mail = new MailInfo
            {
                Sender = new EmailAddress()
                {
                    Name = "New Sender",
                    Email = "steel.punch0@gmail.com"
                },
                Subject = "Subject",
                Body = "Body text",
                Receiver = new EmailAddress()
                {
                    Name = "Receiver",
                    Email = "watchster.integration@gmail.com"
                }
            };
            try
            {
                _sendGridService.SendMailAsync(mail).Wait();
            }
            catch (Exception e)
            {
                Assert.Pass();
            }
            Assert.Fail();
        }

        [Test]
        public void Given_Mail_WhereSenderAddressIsChangedToConfigurationAdreess_Then_SendMailAsyncShouldSendEmail()
        {
            var mail = new MailInfo
            {
                Sender = new EmailAddress()
                {
                    Name = "New Sender",
                    Email = "watchster.integration@gmail.com"
                },
                Subject = "Subject",
                Body = "Body text",
                Receiver = new EmailAddress()
                {
                    Name = "Receiver",
                    Email = "watchster.integration@gmail.com"
                }
            };
            try
            {
                _sendGridService.SendMailAsync(mail).Wait();
            }
            catch (Exception e)
            {
                Assert.Fail();
            }
            Assert.Pass();
        }
    }

}
