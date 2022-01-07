using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Watchster.Application.Interfaces;

namespace Watchster.Application.Features.Queries
{
    public class GetRatingQueryHandler : IRequestHandler<GetRatingQuery, int>
    {
        private readonly IRatingRepository repository;

        public GetRatingQueryHandler(IRatingRepository repository)
        {
            this.repository = repository;
        }

        public Task<int> Handle(GetRatingQuery request, CancellationToken cancellationToken)
        {
            var rating = repository.Query(rating => rating.UserId == request.UserId && rating.MovieId == request.MovieId)
                .FirstOrDefault();

            if (rating == null)
            {
                return Task.FromResult(0);
            }

            return Task.FromResult((int)rating.RatingValue);
        }
    }
}
