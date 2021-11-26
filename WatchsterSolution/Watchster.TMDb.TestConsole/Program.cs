using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using Watchster.TMDb.Services;

namespace Watchster.TMDb.TestConsole
{
    static class Program
    {
        static void Main()
        {
            var services = new ServiceCollection()
                .AddTMDb()
                .AddLogging(configure => configure.AddConsole())
                .Configure<LoggerFilterOptions>(options => options.MinLevel = LogLevel.Information)
                .BuildServiceProvider();

            var TMDbService = services.GetRequiredService<ITMDbMovieDiscoverService>();

            TestGetMovie(TMDbService);
            TestGetMoviesAfterDate(TMDbService);
        }

        private static void TestGetMovie(ITMDbMovieDiscoverService TMDbService)
        {
            string id = "47964";
            var movie = TMDbService.GetMovie(id);
            Console.WriteLine($"Movie Title: {movie.Title}");
            Console.WriteLine($"Movie overview: {movie.Overview}");
        }

        private static void TestGetMoviesAfterDate(ITMDbMovieDiscoverService TMDbService)
        {
            var dateString = "10/1/2021 8:30:00 AM";
            DateTime date = DateTime.Parse(dateString,
                                      System.Globalization.CultureInfo.InvariantCulture);
            var movies = TMDbService.GetMoviesAfterDate(date);
            foreach (var movie in movies)
            {
                Console.WriteLine(movie.Title + " " + movie.ReleaseDate);
            }
        }
    }
}
