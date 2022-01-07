using System;

namespace Watchster.Application.Models
{
    public class MovieDetails
    {
        public int Id { get; set; }

        public int TMDbId { get; set; }

        public string Title { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public string Genres { get; set; }

        public string Overview { get; set; }
    }
}
