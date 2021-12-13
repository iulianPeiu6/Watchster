using MediatR;
using System.Collections.Generic;
using Watchster.Application.Models;

namespace Watchster.Application.Features.Queries
{
    public class GetAllMoviesQuery : IRequest<IEnumerable<MovieDetails>>
    {
    }
}
