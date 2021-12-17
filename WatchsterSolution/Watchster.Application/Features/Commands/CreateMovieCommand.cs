using MediatR;
using System;
using System.Collections.Generic;

namespace Watchster.Application.Features.Commands
{
    public class CreateMovieCommand : IRequest<int>
    {
        public int TMDbId { get; set; }
        public string Title { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public IEnumerable<string> Genres { get; set; }
        public string PosterUrl { get; set; }
        public double Popularity { get; set; }
        public double TMDbVoteAverage { get; set; }
        public string Overview { get; set; }
    }
}
