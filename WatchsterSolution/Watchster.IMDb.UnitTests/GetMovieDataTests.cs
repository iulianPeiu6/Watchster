using NUnit.Framework;
using Watchster.IMDb.Services;
using TMDbLib.Client;
using FluentAssertions;
using System;
using FakeItEasy;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Watchster.IMDb.UnitTests
{
    public class GetMovieDataTests
    {
        private readonly TMDbMovieDiscoverService TMDbMovieDiscoverService;
        private readonly TMDbClient TMDbClient;

        public GetMovieDataTests()
        {
            var logger = A.Fake<ILogger<TMDbMovieDiscoverService>>();
            //var config = A.Fake<IOptions<SendGridConfig>>();
            var config = Options.Create(
                new TMDbConfig
                {
                    ApiKey = Faker.Lorem.Sentence()
                });
            //sendGridClient = A.Fake<SendGridClient>();
            TMDbClient = new TMDbClient(config.Value.ApiKey);

            TMDbMovieDiscoverService = new TMDbMovieDiscoverService(logger, config, TMDbClient);
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Given_Date_When_DateIsTooFar_Then_GetMoviesDataAfterGivenDateAsyncShouldThrowThrowAggregateException()
        {
            var dateString = "1/1/2026 8:30:00 AM";
            DateTime date = DateTime.Parse(dateString,
                                      System.Globalization.CultureInfo.InvariantCulture);
            Action action = () => TMDbMovieDiscoverService.GetMoviesDataAfterGivenDateAsync(date).Wait();

            action.Should().Throw<AggregateException>().And.InnerException.Should().BeOfType<AggregateException>();
        }
    }
}