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
    public class GenerateAndSaveResetPasswordCodeCommandHandler : IRequestHandler<GenerateAndSaveResetPasswordCodeCommand, ResetPasswordCodeResult>
    {
        private readonly IUserRepository userRepository;
        private readonly IResetPasswordCodeRepository resetPasswordCodeRepository;

        public GenerateAndSaveResetPasswordCodeCommandHandler(IUserRepository userRepository, IResetPasswordCodeRepository resetPasswordCodeRepository)
        {
            this.userRepository = userRepository;
            this.resetPasswordCodeRepository = resetPasswordCodeRepository;
        }

        public async Task<ResetPasswordCodeResult> Handle(GenerateAndSaveResetPasswordCodeCommand request, CancellationToken cancellationToken)
        {
            var user = userRepository.Query(user => user.Email == request.Email)
                .FirstOrDefault();

            if (user is null)
            {
                return new ResetPasswordCodeResult
                {
                    ResetPasswordCode = null,
                    ErrorMessage = Error.EmailNotFound
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
                ResetPasswordCode = resetPasswordCode
            };
        }
    }
}
