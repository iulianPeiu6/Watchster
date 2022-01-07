using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Watchster.Application.Interfaces;
using Watchster.Domain.Entities;

namespace Watchster.Application.Features.Queries
{
    public class GetLatestReleasedMoviesQueryHandler : IRequestHandler<GetLatestReleasedMoviesQuery, List<Movie>>
    {
        private readonly IMovieRepository movieRepository;

        public GetLatestReleasedMoviesQueryHandler(IMovieRepository movieRepository)
        {
            this.movieRepository = movieRepository;
        }

        public Task<List<Movie>> Handle(GetLatestReleasedMoviesQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(movieRepository.Query(movie => movie.ReleaseDate.Value.AddMonths(3) >= DateTime.Now)
                .ToList());
        }
    }
}
