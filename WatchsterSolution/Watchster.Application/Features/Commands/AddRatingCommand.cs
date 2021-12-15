using MediatR;
using System;
using Watchster.Application.Models;

namespace Watchster.Application.Features.Commands
{
    public class AddRatingCommand : IRequest<AddRatingResponse>
    {
        public Guid userId { get; set; }
        public Guid movieId { get; set; }
        public double rating { get; set; }
    }
}
