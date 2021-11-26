using System;
using System.Collections.Generic;

namespace Watchster.TMDb.Models
{
    public class Movie
    {
        public int ImdbId { get; set; }
        public string Title { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public List<Models.Genre> Genres { get; set; }
        public string Overview { get; set; }
    }
}
