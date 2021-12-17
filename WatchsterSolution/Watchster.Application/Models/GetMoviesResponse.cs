using System.Collections.Generic;
using Watchster.Domain.Entities;

namespace Watchster.Application.Models
{
    public class GetMoviesResponse
    {
        public int TotalPages { get; set; }
        public IList<Movie> Movies { get; set; }
    }
}
