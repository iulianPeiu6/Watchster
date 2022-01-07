using MediatR;
using System.Collections.Generic;
using Watchster.Domain.Entities;

namespace Watchster.Application.Features.Queries
{
    public class GetMostPopularMoviesQuery : IRequest<List<Movie>>
    {
    }
}
