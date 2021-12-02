using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Watchster.Domain.Entities;

namespace Watchster.Application.Features.Commands
{
    public class CreateUserCommand : IRequest <Guid>
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public bool IsSubscribed { get; set; }
    }
}
