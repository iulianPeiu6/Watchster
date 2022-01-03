using System.Collections.Generic;

namespace Watchster.Application.Models
{
    public class GetRecommendationsResponse
    {
        public IList<RecommendationDetails> Recommendations { get; set; }
    }
}
