using MediatR;
using System;
using Watchster.Application.Models;

namespace Watchster.Application.Features.Commands
{
    public class ChangeUserPasswordCommand : IRequest<ChangePasswordResult>
    {
        public String Code { get; set; }
        public String Password { get; set; }
    }

}
