using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Watchster.Application.Interfaces;
using Watchster.Domain.Entities;

namespace Watchster.Application.Features.Commands
{
    public class CreateMovieCommandHandler : IRequestHandler<CreateMovieCommand, int>
    {
        private readonly IMovieRepository repository;

        public CreateMovieCommandHandler(IMovieRepository repository)
        {
            this.repository = repository;
        }

        public async Task<int> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
        {
            var movie = new Movie
            {
                Title = request.Title,
                Overview = request.Overview,
                TMDbId = request.TMDbId,
                ReleaseDate = request.ReleaseDate,
                Genres = string.Join(",", request.Genres)
            };

            await repository.AddAsync(movie);
            return movie.Id;
        }
    }
}
