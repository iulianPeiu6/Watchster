using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Watchster.Application.Interfaces;
using Watchster.Application.Models;
using Watchster.Domain.Entities;

namespace Watchster.Application.Features.Queries
{
    public class GetMovieByIdQueryHandler : IRequestHandler<GetMovieByIdQuery, MovieResult>
    {
        private readonly IMovieRepository repository;

        public GetMovieByIdQueryHandler(IMovieRepository repository)
        {
            this.repository = repository;
        }

        public async Task<MovieResult> Handle(GetMovieByIdQuery request, CancellationToken cancellationToken)
        {
            var movieResult = new MovieResult();
            var movieInstance = await repository.GetByIdAsync(request.Id);
            if (movieInstance is null)
            {
                movieResult.ErrorMessage = Error.MovieNotFound;
                return movieResult;
            }
            movieResult.Movie = movieInstance;
            return movieResult;
        }
    }
}
