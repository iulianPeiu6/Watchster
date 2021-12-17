using MediatR;
using Watchster.Application.Models;

namespace Watchster.Application.Features.Queries
{
    public class GetMovieByIdQuery : IRequest<MovieResult>
    {
        public int Id { get; set; }
    }

}
