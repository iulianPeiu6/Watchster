using MediatR;
using Watchster.Application.Models;

namespace Watchster.Application.Features.Commands
{
    public class ChangeUserPasswordCommand : IRequest<ChangePasswordResult>
    {
        public string Code { get; set; }
        public string Password { get; set; }
    }

}
