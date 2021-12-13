using MediatR;
using System.Collections.Generic;
using Watchster.Domain.Entities;

namespace Watchster.Application.Features.Queries
{
    public class GetAllRatingsQuery : IRequest<IEnumerable<Rating>>
    {
    }
}
