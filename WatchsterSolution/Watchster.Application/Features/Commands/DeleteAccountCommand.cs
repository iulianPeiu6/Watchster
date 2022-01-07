using MediatR;
using System;

namespace Watchster.Application.Features.Commands
{
    public class DeleteAccountCommand : IRequest<Boolean>
    {
        public int UserId { get; set; }
    }

}
