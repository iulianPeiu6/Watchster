using System;
using System.Collections.Generic;

namespace Watchster.TMDb.Models
{
    public class Movie
    {
        public int TMDbId { get; set; }
        public string Title { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
        public string Overview { get; set; }
    }
}
