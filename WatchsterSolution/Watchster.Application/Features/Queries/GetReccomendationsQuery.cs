using MediatR;
using Watchster.Application.Models;

namespace Watchster.Application.Features.Queries
{
    public class GetReccomendationsQuery : IRequest<GetRecommendationsResponse>
    {
        public int UserId { get; set; }
    }
}
