using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Watchster.Application.Interfaces;
using Watchster.Application.Models;
using Watchster.Domain.Entities;

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

        public Task<GetMoviesResponse> Handle(GetMoviesFromPageCommand request, CancellationToken cancellationToken)
        {
            return Task.Run(async () =>
            {
                try
                {
                    int totalPages = await movieRepository.GetTotalPages();

                    List<Movie> movies =  (List<Movie>)await movieRepository.GetMoviesFromPage(request.Page);

                    foreach (var movie in movies)
                    {
                        List<Genre> genres = (List<Genre>)await genreRepository.GetGenresByMovieId(movie.Id);
                        movie.Genres = genres;
                    }

                    return new GetMoviesResponse
                    {
                        TotalPages = totalPages,
                        Movies = movies
                    };
                }
                catch (ArgumentException ex)
                {
                    GetMoviesResponse ErrorResponse = new GetMoviesResponse
                    {
                        ErrorMessage = ex.Message
                    };
                    return ErrorResponse;
                }
            });
        }
    }
}
