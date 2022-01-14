
using Watchster.Domain.Entities;

namespace Watchster.Application.Models
{
    public class MovieRecommendation : Movie
    {
        public float Score { get; set; }
    }
}
