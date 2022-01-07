using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Watchster.Application.Interfaces;

namespace Watchster.Application.Features.Commands
{
    public class DeleteAccountCommandHandler : IRequestHandler<DeleteAccountCommand, Boolean>
    {
        private readonly IUserRepository userRepository;

        public DeleteAccountCommandHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public Task<bool> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
        {
            return Task.Run(async () =>
            {
                var userInstance = await userRepository.GetByIdAsync(request.UserId);

                if (userInstance is null)
                {
                    return false;
                }
                await userRepository.Delete(userInstance);
                return true;
            });
        }
    }
}