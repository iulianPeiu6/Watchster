using System;
using System.Collections.Generic;
using Watchster.TMDb.Models;

namespace Watchster.TMDb.Services
{
    public interface ITMDbMovieDiscoverService
    {
        Movie GetMovie(string id);
        List<Movie> GetMoviesBetweenDates(DateTime from, DateTime to);
        (int TotalPages, List<Movie> Movies) GetMoviesBetweenDatesFromPage(DateTime from, DateTime to, int page = 1);
    }
}
