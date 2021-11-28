using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using TMDbLib.Client;
using Watchster.TMDb.Models;

namespace Watchster.TMDb.Services
{
    public class TMDbMovieDiscoverService : ITMDbMovieDiscoverService
    {
        private readonly ILogger<TMDbMovieDiscoverService> logger;
        private readonly TMDbClient TMDbClient;

        public TMDbMovieDiscoverService(ILogger<TMDbMovieDiscoverService> logger, IOptions<TMDbConfig> config)
        {
            this.logger = logger;
            TMDbClient = new TMDbClient(config.Value.ApiKey);
        }

        public Movie GetMovie(string id)
        {
            try
            {
                logger.LogInformation($"Start requesting movie {id} from TMDb");
                var response = this.TMDbClient.GetMovieAsync(id).Result;

                if (response is null)
                {
                    throw new ArgumentException("Movie not found!");
                }

                var movie = new Movie
                {
                    ImdbId = response.Id,
                    Title = response.Title,
                    Genres = response.Genres.Select(genre => new Genre()
                    {
                        TMDbId = genre.Id,
                        Name = genre.Name
                    }).ToList(),
                    ReleaseDate = response.ReleaseDate,
                    Overview = response.Overview
                };
                
                logger.LogInformation($"Movie found: {movie.Title}");
                return movie;
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to receive movie data: {ex.Message}", ex);
                throw;
            }
        }

        public List<Movie> GetMoviesAfterDate(DateTime date)
        {
            var movies = new List<Movie>();
            try
            {
                logger.LogInformation($"Start requesting movies after date {date}");
                var result = TMDbClient.DiscoverMoviesAsync().WherePrimaryReleaseDateIsAfter(date);

                int numOfPages = result.Query().Result.TotalPages;

                for (int page = 0; page < numOfPages; page++)
                {
                    var response = result.Query(page).Result;
                    var moviesFromCurrentPage = response.Results
                        .Select(movie => new Movie
                        {
                            ImdbId = movie.Id,
                            Title = movie.Title,
                            Genres = movie.GenreIds.Select(genre => new Models.Genre()
                            {
                                TMDbId = genre
                            }).ToList(),
                            ReleaseDate = movie.ReleaseDate,
                            Overview = movie.Overview
                        })
                        .ToList();
                    movies.AddRange(moviesFromCurrentPage);
                }

                logger.LogInformation($"{movies.Count} new movie(s) queried");

                return movies;
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to receive movie data: {ex.Message}", ex);
                throw;
            }
        }
    }
}
