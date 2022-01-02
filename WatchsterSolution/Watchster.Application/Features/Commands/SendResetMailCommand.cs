using MediatR;
using Watchster.Domain.Entities;

namespace Watchster.Application.Features.Commands
{
    public class SendResetMailCommand : IRequest<string>
    {
        public ResetPasswordCode Result { get; set; }
        public string Endpoint { get; set; }
    }

}
