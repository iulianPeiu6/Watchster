using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Watchster.Application.Interfaces;
using Watchster.Application.Models;
using Watchster.Domain.Entities;

namespace Watchster.Application.Features.Queries
{
    public class GetMostPopularMoviesQueryHandler : IRequestHandler<GetMostPopularMoviesQuery, List<Movie>>
    {
        private readonly IMovieRepository movieRepository;

        public GetMostPopularMoviesQueryHandler(IMovieRepository movieRepository)
        {
            this.movieRepository = movieRepository;
        }

        public Task<List<Movie>> Handle(GetMostPopularMoviesQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(movieRepository.Query()
                .OrderByDescending(movie => movie.Popularity)
                .Take(100)
                .ToList());
        }
    }
}
