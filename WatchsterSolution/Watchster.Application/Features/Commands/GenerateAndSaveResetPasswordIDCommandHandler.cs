using MediatR;
using shortid;
using shortid.Configuration;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Watchster.Application.Interfaces;
using Watchster.Application.Models;
using Watchster.Domain.Entities;

namespace Watchster.Application.Features.Commands
{
    public class GenerateAndSaveResetPasswordIDCommandHandler : IRequestHandler<GenerateAndSaveResetPasswordIDCommand, ResetPasswordCodeResult>
    {
        private readonly IUserRepository userRepository;
        private readonly IResetPasswordCodeRepository resetPasswordCodeRepository;

        public GenerateAndSaveResetPasswordIDCommandHandler(IUserRepository userRepository, IResetPasswordCodeRepository resetPasswordCodeRepository)
        {
            this.userRepository = userRepository;
            this.resetPasswordCodeRepository = resetPasswordCodeRepository;
        }

        public Task<ResetPasswordCodeResult> Handle(GenerateAndSaveResetPasswordIDCommand request, CancellationToken cancellationToken)
        {
            return Task.Run(async () =>
            {
                var user = userRepository.Query(user => user.Email == request.Email)
                    .FirstOrDefault();

                if (user is null)
                {
                    return new ResetPasswordCodeResult
                    {
                        resetPasswordCode = null,
                        ErrorMessage = "Email does not exist in database"
                    };
                }

                var options = new GenerationOptions
                {
                    Length = 12
                };
                string code = ShortId.Generate(options);

                DateTime expirationTime = DateTime.Now.AddHours(1);

                var resetPasswordCode = new ResetPasswordCode
                {
                    Code = code,
                    expirationDate = expirationTime,
                    Email = request.Email
                };

                await resetPasswordCodeRepository.AddAsync(resetPasswordCode);

                return new ResetPasswordCodeResult
                {
                    resetPasswordCode = resetPasswordCode
                };

            });
        }
    }
}
