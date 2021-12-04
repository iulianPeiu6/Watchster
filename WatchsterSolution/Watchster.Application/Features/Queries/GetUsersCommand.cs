using MediatR;
using System.Collections.Generic;
using Watchster.Domain.Entities;

namespace Watchster.Application.Features.Queries
{
    class GetUsersCommand : IRequest<IEnumerable<User>>
    {
    }
}
