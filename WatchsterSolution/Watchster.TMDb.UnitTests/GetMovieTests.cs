namespace Watchster.TMDb.UnitTests
{
    public class GetMovieDataTests
    {
        /*
        private readonly TMDbMovieDiscoverService TMDbService;
        
        public GetMovieDataTests()
        {
            
            var logger = A.Fake<ILogger<TMDbMovieDiscoverService>>();
            var configuration = new ConfigurationBuilder()
                .AddUserSecrets<TMDbConfig>(true)
                .Build();
            var config = Options.Create(
                new TMDbConfig
                {
                    ApiKey = configuration["ApiKey"],
                });
            TMDbService = new TMDbMovieDiscoverService(logger, config);
            TMDbService = A.Fake<TMDbMovieDiscoverService>();
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Given_MovieId_When_MovieIdIsNotValid_Then_GetMovieShouldThrowArgumentException()
        {
            string id = "234567897";

            Action result = () => TMDbService.GetMovie(id);

            result.Should().Throw<ArgumentException>();
        }

        [Test]

        public void Given_Dates_When_DatesAreInvalid_Then_GetMoviesBetweenDatesShouldReturnAnEmptyList()
        {
            DateTime from = new DateTime(3100, 1, 1);
            DateTime to = new DateTime(3090, 12, 30);

            var result = TMDbService.GetMoviesBetweenDates(from, to);

            Assert.True(result.Count == 0);
        }

        [Test]
        public void Given_Dates_When_DatesAreValid_Then_GetMoviesBetweenDatesShouldReturnANonEmptyList()
        {
            DateTime from = new DateTime(2000, 1, 1);
            DateTime to = new DateTime(2000, 2, 1);

            var result = TMDbService.GetMoviesBetweenDates(from, to);

            Assert.True(result.Count != 0);
        }*/
    }
}