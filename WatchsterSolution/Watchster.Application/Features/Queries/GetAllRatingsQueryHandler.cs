using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Watchster.Application.Interfaces;
using Watchster.Domain.Entities;

namespace Watchster.Application.Features.Queries
{
    public class GetAllRatingsQueryHandler : IRequestHandler<GetAllRatingsQuery, IEnumerable<Rating>>
    {
        private readonly IRatingRepository repository;

        public GetAllRatingsQueryHandler(IRatingRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<Rating>> Handle(GetAllRatingsQuery request, CancellationToken cancellationToken)
        {
            return await repository.GetAllAsync();
        }
    }
}
