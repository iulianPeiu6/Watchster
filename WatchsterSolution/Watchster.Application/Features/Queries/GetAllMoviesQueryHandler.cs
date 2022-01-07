using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Watchster.Application.Interfaces;
using Watchster.Application.Models;

namespace Watchster.Application.Features.Queries
{
    public class GetAllMoviesQueryHandler : IRequestHandler<GetAllMoviesQuery, IEnumerable<MovieDetails>>
    {
        private readonly IMovieRepository movieRepository;

        public GetAllMoviesQueryHandler(IMovieRepository movieRepository)
        {
            this.movieRepository = movieRepository;
        }

        public async Task<IEnumerable<MovieDetails>> Handle(GetAllMoviesQuery request, CancellationToken cancellationToken)
        {
            return (await movieRepository.GetAllAsync())
                .Select(movie =>
                new MovieDetails
                {
                    Id = movie.Id,
                    TMDbId = movie.TMDbId,
                    Title = movie.Title,
                    Genres = movie.Genres,
                    Overview = movie.Overview,
                    ReleaseDate = movie.ReleaseDate
                });
        }
    }
}
