using System.Collections.Generic;

namespace Watchster.Application.Models
{
    public class GetMoviesResponse
    {
        public int TotalPages { get; set; }
        public IList<MovieDetails> Movies { get; set; }
    }
}
