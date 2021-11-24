using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Watchster.MLUtil.Models;
using Watchster.MLUtil.Services.Interfaces;

namespace Watchster.MLUtil.TestConsole
{
    class Program
    {
        static void Main()
        {
            var services = new ServiceCollection()
                .AddMLUtil()
                .AddLogging(configure => configure.AddConsole())
                .Configure<LoggerFilterOptions>(options => options.MinLevel = LogLevel.Information)
                .BuildServiceProvider();

            TestMovieRecommender(services);
        }

        private static void TestMovieRecommender(ServiceProvider services)
        {
            var movieRecommender = services.GetRequiredService<IMovieRecommender>();

            var logger = services.GetRequiredService<ILogger<Program>>();

            var movieRatingToPredict = new MovieRating()
            {
                UserId = 1,
                MovieId = 7438,
            };

            var moviePrediction = movieRecommender.PredictMovieRating(movieRatingToPredict);
            logger.LogInformation($"{moviePrediction.Score}");
        }
    }
}
