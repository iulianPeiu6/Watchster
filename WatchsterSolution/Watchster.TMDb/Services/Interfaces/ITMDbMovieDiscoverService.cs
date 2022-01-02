using System;
using System.Collections.Generic;
using Watchster.TMDb.Models;

namespace Watchster.TMDb.Services
{
    public interface ITMDbMovieDiscoverService
    {
        (int TotalPages, List<Movie> Movies) GetMoviesBetweenDatesFromPage(DateTime from, DateTime to, int page = 1);
    }
}
