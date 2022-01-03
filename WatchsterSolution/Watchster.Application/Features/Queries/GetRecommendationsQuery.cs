using MediatR;
using Watchster.Application.Models;

namespace Watchster.Application.Features.Queries
{
    public class GetRecommendationsQuery : IRequest<GetRecommendationsResponse>
    {
        public int UserId { get; set; }
    }
}
