using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Watchster.Application.Models;
using Watchster.Domain.Entities;

namespace Watchster.Application.Features.Queries
{
    public class GetReccomendationsQuery: IRequest<GetRecommendationsResponse>
    {
        public int UserId { get; set; }
    }
}
