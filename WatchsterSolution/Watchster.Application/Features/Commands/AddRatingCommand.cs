using MediatR;
using System;
using Watchster.Application.Models;

namespace Watchster.Application.Features.Commands
{
    public class AddRatingCommand : IRequest<AddRatingResponse>
    {
        public int userId { get; set; }
        public int movieId { get; set; }
        public double rating { get; set; }
    }
}
