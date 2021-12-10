using System;
using System.Collections.Generic;

namespace Watchster.Application.Models
{
    public class MovieDetails
    {
        public int TMDbId { get; set; }

        public string Title { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public IList<string> Genres { get; set; }

        public decimal AverageRating { get; set; }

        public string Overview { get; set; }
    }
}
