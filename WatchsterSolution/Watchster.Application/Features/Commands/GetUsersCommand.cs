using MediatR;
using System.Collections.Generic;
using Watchster.Domain.Entities;

namespace Watchster.Application.Features.Commands
{
    class GetUsersCommand : IRequest<IEnumerable<User>>
    {
    }
}
