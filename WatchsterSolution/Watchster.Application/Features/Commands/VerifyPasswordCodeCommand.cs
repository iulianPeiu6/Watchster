using MediatR;
using System;

namespace Watchster.Application.Features.Commands
{
    public class VerifyPasswordCodeCommand : IRequest<Boolean>
    {
        public String Code { get; set; }
    }

}
