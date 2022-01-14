using System.Collections.Generic;

namespace Watchster.Application.Models
{
    public class GetRecommendationsResponse
    {
        public IList<MovieRecommendation> Recommendations { get; set; }
    }
}
