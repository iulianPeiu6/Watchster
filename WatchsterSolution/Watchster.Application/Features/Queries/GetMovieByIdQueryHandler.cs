using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Watchster.Application.Interfaces;
using Watchster.Domain.Entities;

namespace Watchster.Application.Features.Queries
{
    public class GetMovieByIdQueryHandler : IRequestHandler<GetMovieByIdQuery, Movie>
    {
        private readonly IMovieRepository repository;

        public GetMovieByIdQueryHandler(IMovieRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Movie> Handle(GetMovieByIdQuery request, CancellationToken cancellationToken)
        {
            var movie = await repository.GetByIdAsync(request.guid);
            return movie;
        }
    }
}
