using FakeItEasy;
using Faker;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Watchster.Application.Features.Commands;
using Watchster.Application.Models;
using Watchster.SendGrid.Models;
using Watchster.SendGrid.Services;

namespace Watchster.Application.UnitTests.Features.Commands
{
    public class SendMovieRecommendationsViaEmailCommandTests
    {
        private readonly SendMovieRecommendationsViaEmailCommandHandler handler;
        private readonly ISendGridService sendGridService;
        private readonly string invalidEmail = "";

        public SendMovieRecommendationsViaEmailCommandTests()
        {
            var fakeSendGridService = new Fake<ISendGridService>();

            fakeSendGridService.CallsTo(sg => sg.SendMailAsync(A<MailInfo>.That.Matches(x => x.Receiver.Email == invalidEmail)))
                .Throws<ArgumentException>();
            sendGridService = fakeSendGridService.FakedObject;
            handler = new SendMovieRecommendationsViaEmailCommandHandler(sendGridService);
        }

        [SetUp]
        public void SetUp()
        {
            Fake.ClearRecordedCalls(sendGridService);
        }

        [TearDown]
        public void TearDown()
        {
            Fake.ClearRecordedCalls(sendGridService);
        }

        [Test]
        public async Task Given_SendMovieRecommendationsViaEmailCommand_When_RecommendationsAreEmpty_Then_EmailIsNotSend()
        {
            //arrage
            var command = new SendMovieRecommendationsViaEmailCommand
            {
                Recommendations = new List<MovieRecommendation>(),
                ToEmailAddress = Internet.Email()
            };

            //act
            var response = await handler.Handle(command, default);

            //assert
            response.Should().Be(false);
        }

        [Test]
        public async Task Given_SendMovieRecommendationsViaEmailCommand_When_ToEmailAddressIsInvalid_Then_EmailIsNotSend()
        {
            //arrage
            var command = new SendMovieRecommendationsViaEmailCommand
            {
                Recommendations = new List<MovieRecommendation>() 
                { 
                    new MovieRecommendation() 
                },
                ToEmailAddress = invalidEmail
            };

            //act
            var response = await handler.Handle(command, default);

            //assert
            response.Should().Be(false);
        }

        [Test]
        public async Task Given_SendMovieRecommendationsViaEmailCommand_When_Valid_Then_EmailIsSend()
        {
            //arrage
            var command = new SendMovieRecommendationsViaEmailCommand
            {
                Recommendations = new List<MovieRecommendation>()
                {
                    new MovieRecommendation()
                },
                ToEmailAddress = Internet.Email(),
            };

            //act
            var response = await handler.Handle(command, default);

            //assert
            response.Should().Be(true);
            A.CallTo(() => sendGridService.SendMailAsync(A<MailInfo>._)).MustHaveHappenedOnceExactly();
        }
    }
}
