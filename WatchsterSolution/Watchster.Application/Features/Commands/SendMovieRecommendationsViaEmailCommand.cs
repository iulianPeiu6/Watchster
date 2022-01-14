using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Watchster.Application.Models;

namespace Watchster.Application.Features.Commands
{
    public class SendMovieRecommendationsViaEmailCommand : IRequest<bool>
    {
        public IEnumerable<MovieRecommendation> Recommendations { get; set; }

        public string ToEmailAddress { get; set; }
    }
}
