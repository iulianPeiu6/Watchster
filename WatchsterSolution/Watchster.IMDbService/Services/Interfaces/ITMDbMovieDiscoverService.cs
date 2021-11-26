using System.Threading.Tasks;
using System;
using TMDbLib.Objects.Movies;
using TMDbLib.Objects.Search;
using System.Collections.Generic;

namespace Watchster.IMDb.Services
{
    public interface ITMDbMovieDiscoverService
    {
        Task<Movie> GetSingleMovieDataAsync(string id);
        Task<List<SearchMovie>> GetMoviesDataAfterGivenDateAsync(DateTime date);
    }
}
