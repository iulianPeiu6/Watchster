using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Watchster.Application.Interfaces;

namespace Watchster.Application.Features.Commands
{
    public class VerifyPasswordCodeCommandHandler : IRequestHandler<VerifyPasswordCodeCommand, Boolean>
    {
        private readonly IUserRepository userRepository;
        private readonly IResetPasswordCodeRepository resetPasswordCodeRepository;

        public VerifyPasswordCodeCommandHandler(IUserRepository userRepository, IResetPasswordCodeRepository resetPasswordCodeRepository)
        {
            this.userRepository = userRepository;
            this.resetPasswordCodeRepository = resetPasswordCodeRepository;
        }
        public Task<bool> Handle(VerifyPasswordCodeCommand request, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                var user = resetPasswordCodeRepository.Query(user => user.Code == request.Code)
                    .FirstOrDefault();

                if (user is null)
                {
                    return false;
                }

                return true;

            });
        }
    }

}
