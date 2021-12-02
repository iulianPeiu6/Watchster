using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Watchster.Domain.Entities;

namespace Watchster.Application.Features.Commands
{
    class GetUsersCommand : IRequest<IEnumerable<User>>
    {
    }
}
