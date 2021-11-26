using System;
using System.Collections.Generic;
using Watchster.TMDb.Models;

namespace Watchster.TMDb.Services
{
    public interface ITMDbMovieDiscoverService
    {
        Movie GetMovie(string id);
        List<Movie> GetMoviesAfterDate(DateTime date);
    }
}
