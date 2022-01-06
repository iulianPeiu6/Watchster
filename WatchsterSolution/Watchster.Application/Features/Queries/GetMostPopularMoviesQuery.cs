using MediatR;
using System.Collections.Generic;
using Watchster.Application.Models;
using Watchster.Domain.Entities;

namespace Watchster.Application.Features.Queries
{
    public class GetMostPopularMoviesQuery : IRequest<List<Movie>>
    {
    }
}
