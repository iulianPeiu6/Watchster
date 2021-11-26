using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using TMDbLib.Objects.Movies;
using TMDbLib.Objects.Search;
using Watchster.IMDb.Services;
using System.Collections.Generic;

namespace Watchster.IMDb.TestConsole
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

            TestSingleMovieData(TMDbService);
            //TestPageMovieData(TMDbService);
        }

        private async static void TestSingleMovieData(ITMDbMovieDiscoverService TMDbService)
        {
            string id = "47964";
            Movie movie = await TMDbService.GetSingleMovieDataAsync(id);
            Console.WriteLine($"Movie Title: {movie.Title}");
            Console.WriteLine($"Movie overview: {movie.Overview}");
        }

        private async static void TestPageMovieData(ITMDbMovieDiscoverService TMDbService)
        {
            var dateString = "1/1/2021 8:30:00 AM";
            DateTime date = DateTime.Parse(dateString,
                                      System.Globalization.CultureInfo.InvariantCulture);
            List<SearchMovie> pages = await TMDbService.GetMoviesDataAfterGivenDateAsync(date);
            foreach (SearchMovie result in pages)
            {
                Console.WriteLine(result.Title + " " + result.ReleaseDate);
            }
        }
    }
}
