using MediatR;
using Watchster.Application.Models;

namespace Watchster.Application.Features.Queries
{
    public class GetMoviesFromPageCommand : IRequest<GetMoviesResponse>
    {
        public int Page { get; set; }
    }
}
