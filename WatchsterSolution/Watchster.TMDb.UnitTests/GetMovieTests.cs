using NUnit.Framework;
using Watchster.TMDb.Services;

namespace Watchster.TMDb.UnitTests
{
    public class GetMovieDataTests
    {
        private readonly TMDbMovieDiscoverService tmdbMovieDiscoverService;

        public GetMovieDataTests()
        {
            //tmdbMovieDiscoverService = A.Fake<TMDbMovieDiscoverService>();
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Given_Date_When_DateIsInTheFuture_Then_GetMoviesAfterDateShouldThrowException()
        {
            //var dateString = "1/1/2026 8:30:00 AM";
            //DateTime date = DateTime.Parse(dateString,
            //                          System.Globalization.CultureInfo.InvariantCulture);
            //Action action = () => tmdbMovieDiscoverService.GetMoviesAfterDate(date);

            //action.Should().Throw<AggregateException>().And.InnerException.Should().BeOfType<AggregateException>();
        }
    }
}