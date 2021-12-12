using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Watchster.Application.Interfaces;
using Watchster.Application.Models;

namespace Watchster.Application.Features.Queries
{
    public class GetMoviesFromPageCommandHandler : IRequestHandler<GetMoviesFromPageCommand, GetMoviesResponse>
    {
        private readonly IMovieRepository movieRepository;

        public GetMoviesFromPageCommandHandler(IMovieRepository movieRepository)
        {
            this.movieRepository = movieRepository;
        }

        public async Task<GetMoviesResponse> Handle(GetMoviesFromPageCommand request, CancellationToken cancellationToken)
        {
            int totalPages = await movieRepository.GetTotalPages();

            var movies = await movieRepository.GetMoviesFromPage(request.Page);

            return new GetMoviesResponse
            {
                TotalPages = totalPages,
                Movies = movies.Select(movie => new MovieDetails
                {
                    TMDbId = movie.TMDbId,
                    Title = movie.Title,
                    ReleaseDate = movie.ReleaseDate,
                    Genres = movie.Genres,
                    Overview = movie.Overview,
                }).ToList(),
            };
        }
    }
}
