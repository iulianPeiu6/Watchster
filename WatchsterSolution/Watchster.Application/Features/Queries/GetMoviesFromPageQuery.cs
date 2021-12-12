using MediatR;
using Watchster.Application.Models;

namespace Watchster.Application.Features.Queries
{
    public class GetMoviesFromPageQuery : IRequest<GetMoviesResponse>
    {
        public int Page { get; set; }
    }
}
