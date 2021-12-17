using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Watchster.Application.Interfaces;
using Watchster.Application.Models;

namespace Watchster.Application.Features.Queries
{
    public class GetMoviesFromPageQueryHandler : IRequestHandler<GetMoviesFromPageQuery, GetMoviesResponse>
    {
        private readonly IMovieRepository movieRepository;

        public GetMoviesFromPageQueryHandler(IMovieRepository movieRepository)
        {
            this.movieRepository = movieRepository;
        }

        public async Task<GetMoviesResponse> Handle(GetMoviesFromPageQuery request, CancellationToken cancellationToken)
        {
            int totalPages = await movieRepository.GetTotalPages();

            var movies = await movieRepository.GetMoviesFromPage(request.Page);

            return new GetMoviesResponse
            {
                TotalPages = totalPages,
                Movies = movies
            };
            //return new GetMoviesResponse
            //{
            //    TotalPages = totalPages,
            //    Movies = movies.Select(movie => new MovieDetails
            //    {
            //        Id = movie.Id,
            //        TMDbId = movie.TMDbId,
            //        Title = movie.Title,
            //        ReleaseDate = movie.ReleaseDate,
            //        Genres = movie.Genres,
            //        Overview = movie.Overview,
            //    }).ToList(),
            //};
        }
    }
}
