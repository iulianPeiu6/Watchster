using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Watchster.Application.Models;

namespace Watchster.Application.Features.Queries
{
    public class GetReccomendationsQuery: IRequest<GetReccomendationsResponse>
    {
        public Guid UserId { get; set; }
    }
}
