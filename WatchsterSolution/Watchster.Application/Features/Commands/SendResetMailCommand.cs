using MediatR;
using System;
using Watchster.Domain.Entities;

namespace Watchster.Application.Features.Commands
{
    public class SendResetMailCommand : IRequest<String>
    {
        public ResetPasswordCode Result { get; set; }
        public String Endpoint { get; set; }
    }

}
