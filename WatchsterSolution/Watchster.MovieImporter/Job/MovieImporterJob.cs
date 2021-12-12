using MediatR;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Watchster.Application.Features.Commands;
using Watchster.Application.Features.Queries;
using Watchster.TMDb.Models;
using Watchster.TMDb.Services;

namespace Watchster.MovieImporter.Job
{
    public class MovieImporterJob : IJob
    {
        private readonly ILogger<MovieImporterJob> logger;
        private readonly ITMDbMovieDiscoverService movieDiscover;
        private readonly IMediator mediator;
        private Domain.Entities.AppSettings movieImporterSettings;
        private DateTime CurrentDateTime;

        public MovieImporterJob(
            ILogger<MovieImporterJob> logger,
            ITMDbMovieDiscoverService movieDiscover,
            IMediator mediator)
        {
            this.logger = logger;
            this.movieDiscover = movieDiscover;
            this.mediator = mediator;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            logger.LogInformation("Starting importing new released movies");

            CurrentDateTime = DateTime.Now;

            var lastSyncDateTime = await GetLastSyncDateTime();

            var numOfImportedMovies = await ImportNewMoviesAfterDateAsync(lastSyncDateTime);

            await UpdateLastSyncDateTime(CurrentDateTime);

            logger.LogInformation($"Ended importing new released movies. {numOfImportedMovies} new movie(s) were imported.");
        }

        private async Task<DateTime> GetLastSyncDateTime()
        {
            var query = new GetAppSettingsBySectionAndParameterQuery()
            {
                Section = "MovieImporter",
                Parameter = "LastSyncDate"
            };

            movieImporterSettings = await mediator.Send(query);
            DateTime lastSyncDate;
            if (movieImporterSettings.Value is null)
            {
                lastSyncDate = DateTime.MinValue;
            }
            else
            {
                lastSyncDate = DateTime.Parse(movieImporterSettings.Value, CultureInfo.InvariantCulture);
            }

            return lastSyncDate;
        }

        private async Task<int> ImportNewMoviesAfterDateAsync(DateTime lastSyncDateTime)
        {
            var result = movieDiscover.GetMoviesBetweenDatesFromPage(lastSyncDateTime, CurrentDateTime);
            await ImportMovies(result.Movies);
            int numOfMoviesImported = result.Movies.Count;

            foreach (var page in Enumerable.Range(2, result.TotalPages - 1))
            {
                var movies = movieDiscover.GetMoviesBetweenDatesFromPage(lastSyncDateTime, CurrentDateTime, page).Movies;
                await ImportMovies(movies);
                numOfMoviesImported += movies.Count;
            }
            return numOfMoviesImported;
        }

        private async Task ImportMovies(List<Movie> movies)
        {
            foreach (var movie in movies)
            {
                var command = new CreateMovieCommand
                {
                    Title = movie.Title,
                    Overview = movie.Overview,
                    TMDbId = movie.TMDbId,
                    ReleaseDate = movie.ReleaseDate,
                    Genres = movie.Genres.Select(genre => genre.Name)
                };
                await mediator.Send(command);
            }
        }

        private async Task UpdateLastSyncDateTime(DateTime currentDateTime)
        {
            var command = new UpdateAppSettingsCommand
            {
                Id = movieImporterSettings.Id,
                Section = movieImporterSettings.Section,
                Parameter = movieImporterSettings.Parameter,
                Description = movieImporterSettings.Description,
                Value = currentDateTime.ToString()
            };
            await mediator.Send(command);
        }
    }
}
