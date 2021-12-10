using EzPasswordValidator.Checks;
using EzPasswordValidator.Validators;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Watchster.Application.Interfaces;
using Watchster.Application.Models;

namespace Watchster.Application.Features.Commands
{
    public class ChangeUserPasswordCommandHandler : IRequestHandler<ChangeUserPasswordCommand, ChangePasswordResult>
    {
        private readonly IUserRepository userRepository;
        private readonly IResetPasswordCodeRepository resetPasswordCodeRepository;
        private readonly ICryptographyService cryptography;

        public ChangeUserPasswordCommandHandler(IUserRepository userRepository, IResetPasswordCodeRepository resetPasswordCodeRepository, ICryptographyService cryptography)
        {
            this.userRepository = userRepository;
            this.resetPasswordCodeRepository = resetPasswordCodeRepository;
            this.cryptography = cryptography;
        }
        public Task<ChangePasswordResult> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
        {
            return Task.Run(async () =>
            {
                var userCode = resetPasswordCodeRepository.Query(user => user.Code == request.Code)
                    .FirstOrDefault();

                if (userCode is null)
                {
                    return new ChangePasswordResult
                    {
                        ErrorMessage = Error.WrongPassChangeCode,
                        Status = false
                    };
                }

                var user = userRepository.Query(user => user.Email == userCode.Email)
                    .FirstOrDefault();

                if (user is null)
                {
                    return new ChangePasswordResult
                    {
                        ErrorMessage = Error.WrongPassChangeCode,
                        Status = false
                    };
                }

                if (!PasswordRespectsContraints(request.Password))
                {
                    return new ChangePasswordResult
                    {
                        ErrorMessage = Error.InvalidPass,
                        Status = false
                    };
                }

                user.Password = cryptography.GetPasswordSHA3Hash(request.Password);

                await userRepository.UpdateAsync(user);

                return new ChangePasswordResult
                {
                    Status = true
                };

            });
        }
        private static bool PasswordRespectsContraints(string password)
        {
            var validator = new PasswordValidator(CheckTypes.Length);
            validator.SetLengthBounds(6, 32);
            return validator.Validate(password);
        }
    }

}
