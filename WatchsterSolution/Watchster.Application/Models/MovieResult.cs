using Watchster.Domain.Entities;

namespace Watchster.Application.Models
{
    public class MovieResult
    {
        public Movie Movie { get; set; }
        public string ErrorMessage { get; set; }
    }
}
