using MediatR;
using Watchster.Application.Authentication.Models;

namespace Watchster.Application.Features.Commands
{
    public class AuthenticateUserCommand : IRequest<UserAuthenticationResult>
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
