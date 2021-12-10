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
        private readonly IGenreRepository genreRepository;

        public GetMoviesFromPageCommandHandler(IMovieRepository movieRepository, IGenreRepository genreRepository)
        {
            this.movieRepository = movieRepository;
            this.genreRepository = genreRepository;
        }

        public async Task<GetMoviesResponse> Handle(GetMoviesFromPageCommand request, CancellationToken cancellationToken)
        {
            int totalPages = await movieRepository.GetTotalPages();

            var movies = await movieRepository.GetMoviesFromPage(request.Page);

            foreach (var movie in movies)
            {
                var genres = await genreRepository.GetGenresByMovieId(movie.Id);
                movie.Genres = genres;
            }

            return new GetMoviesResponse
            {
                TotalPages = totalPages,
                Movies = movies.Select(movie => new MovieDetails
                {
                    TMDbId = movie.TMDbId,
                    Title = movie.Title,
                    ReleaseDate = movie.ReleaseDate,
                    Genres = movie.Genres.Select(genre => genre.Name).ToList(),
                    Overview = movie.Overview,
                }).ToList(),
            };
        }
    }
}
