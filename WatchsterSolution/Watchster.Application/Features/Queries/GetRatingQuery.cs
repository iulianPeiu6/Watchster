using MediatR;

namespace Watchster.Application.Features.Queries
{
    public class GetRatingQuery : IRequest<int>
    {
        public int UserId { get; set; }
        public int MovieId { get; set; }
    }
}
