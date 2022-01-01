using MediatR;
using Watchster.Application.Models;

namespace Watchster.Application.Features.Commands
{
    public class AddRatingCommand : IRequest<AddRatingResponse>
    {
        public int UserId { get; set; }
        public int MovieId { get; set; }
        public double Rating { get; set; }
    }
}
