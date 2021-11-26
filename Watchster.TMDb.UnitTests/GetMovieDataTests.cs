using NUnit.Framework;
using Watchster.TMDb.Services;
using TMDbLib.Client;
using FluentAssertions;
using System;
using FakeItEasy;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Watchster.TMDb.UnitTests
{
    public class GetMovieDataTests
    {
        private readonly TMDbMovieDiscoverService tmdbMovieDiscoverService;
        private readonly TMDbClient tmdbClient;

        public GetMovieDataTests()
        {
            var logger = A.Fake<ILogger<TMDbMovieDiscoverService>>();
            var config = A.Fake<IOptions<TMDbConfig>>();
            //var config = Options.Create(
            //    new TMDbConfig
            //    {
            //        ApiKey = Faker.Lorem.Sentence()
            //    });
            tmdbClient = A.Fake<TMDbClient>();

            tmdbMovieDiscoverService = new TMDbMovieDiscoverService(logger, config, tmdbClient);
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Given_Date_When_DateIsTooFar_Then_GetMoviesAfterDateShouldThrowThrowAggregateException()
        {
            var dateString = "1/1/2026 8:30:00 AM";
            DateTime date = DateTime.Parse(dateString,
                                      System.Globalization.CultureInfo.InvariantCulture);
            Action action = () => tmdbMovieDiscoverService.GetMoviesAfterDate(date);

            action.Should().Throw<AggregateException>().And.InnerException.Should().BeOfType<AggregateException>();
        }
    }
}