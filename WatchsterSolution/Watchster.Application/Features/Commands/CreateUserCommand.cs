using MediatR;
using Watchster.Application.Models;

namespace Watchster.Application.Features.Commands
{
    public class CreateUserCommand : IRequest<UserRegistrationResult>
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public bool IsSubscribed { get; set; }
    }
}
