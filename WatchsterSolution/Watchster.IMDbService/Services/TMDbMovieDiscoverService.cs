using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using System;
using TMDbLib.Client;
using TMDbLib.Objects.Movies;
using TMDbLib.Objects.Search;
using System.Collections.Generic;

namespace Watchster.IMDb.Services
{
    public class TMDbMovieDiscoverService : ITMDbMovieDiscoverService
    {
        private readonly ILogger<TMDbMovieDiscoverService> logger;
        private readonly TMDbConfig config;
        private readonly TMDbClient TMDbClient;

        public TMDbMovieDiscoverService(ILogger<TMDbMovieDiscoverService> logger, IOptions<TMDbConfig> config)
        {
            this.logger = logger;
            this.config = config.Value;
            this.TMDbClient = new TMDbClient(this.config.ApiKey);
        }

        public TMDbMovieDiscoverService(ILogger<TMDbMovieDiscoverService> logger, IOptions<TMDbConfig> config, TMDbClient TMDbClient)
        {
            this.logger = logger;
            this.config = config.Value;
            this.TMDbClient = TMDbClient;
        }

        public async Task<Movie> GetSingleMovieDataAsync(string id)
        {
            try
            {
                logger.LogInformation("Sending request for a movie");
                Movie movie = this.TMDbClient.GetMovieAsync(id).Result;
                if(movie is null)
                {
                    throw new ArgumentException($"Error finding movie using id {id}");
                }
                logger.LogInformation($"Movie found: {movie.Title}");
                return movie;
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to receive movie data: {ex.Message}", ex);
                throw;
            }
        }
        public async Task<List<SearchMovie>> GetMoviesDataAfterGivenDateAsync(DateTime date)
        {
            try
            {
                logger.LogInformation($"Sending request for movies after date {date}");
                var result = this.TMDbClient.DiscoverMoviesAsync().WherePrimaryReleaseDateIsAfter(date);
                logger.LogInformation($"Movies found");
                if (result.Query().Result.TotalResults == 0)
                {
                    throw new ArgumentException("Error finding movies");
                }
                List<SearchMovie> Movies = new List<SearchMovie>();
                for(int i = 1; i < result.Query().Result.TotalResults; i++)
                {
                    var page = result.Query(i).Result;
                    foreach (SearchMovie pageMovie in page.Results)
                        Movies.Add(pageMovie);
                }
                if(Movies.Count == 0)
                {
                    throw new ArgumentException("Error finding movies");
                }
                return Movies;
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to receive movie data: {ex.Message}", ex);
                throw;
            }
        }
    }
}
