using MediatR;
using System.Collections.Generic;
using Watchster.Application.Models;

namespace Watchster.Application.Features.Commands
{
    public class SendMovieRecommendationsViaEmailCommand : IRequest<bool>
    {
        public IEnumerable<MovieRecommendation> Recommendations { get; set; }

        public string ToEmailAddress { get; set; }
    }
}
