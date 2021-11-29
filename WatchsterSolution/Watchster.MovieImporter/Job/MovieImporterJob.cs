using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Watchster.TMDb.Models;
using Watchster.TMDb.Services;

namespace Watchster.MovieImporter.Job
{
    public class MovieImporterJob : IJob
    {
        private readonly ILogger<MovieImporterJob> logger;
        private readonly ITMDbMovieDiscoverService movieDiscover;
        private DateTime CurrentDateTime;

        public MovieImporterJob(ILogger<MovieImporterJob> logger, ITMDbMovieDiscoverService movieDiscover)
        {
            this.logger = logger;
            this.movieDiscover = movieDiscover;
        }

        public Task Execute(IJobExecutionContext context)
        {
            logger.LogInformation("Starting importing new released movies");

            CurrentDateTime = DateTime.Now;

            var lastSyncDateTime = GetLastSyncDateTime();

            var numOfImportedMovies = ImportNewMoviesAfterDate(lastSyncDateTime);

            UpdateLastSyncDateTime(CurrentDateTime);

            logger.LogInformation($"Ended importing new released movies. {numOfImportedMovies} new movie(s) were imported.");
            return Task.CompletedTask;
        }

        private DateTime GetLastSyncDateTime()
        {
            return DateTime.Parse("10/1/2021 8:30:00 AM", System.Globalization.CultureInfo.InvariantCulture);
        }

        private int ImportNewMoviesAfterDate(DateTime lastSyncDateTime)
        {
            var result = movieDiscover.GetMoviesBetweenDatesFromPage(lastSyncDateTime, CurrentDateTime);
            ImportMovies(result.Movies);

            foreach (var page in Enumerable.Range(2, result.TotalPages))
            {
                var movies = movieDiscover.GetMoviesBetweenDatesFromPage(lastSyncDateTime, CurrentDateTime, page).Movies;
                ImportMovies(movies);
            }
            return result.Movies.Count;
        }

        private void ImportMovies(List<Movie> movies)
        {
            throw new NotImplementedException();
        }

        private void UpdateLastSyncDateTime(DateTime currentDateTime)
        {
            throw new NotImplementedException();
        }
    }
}
