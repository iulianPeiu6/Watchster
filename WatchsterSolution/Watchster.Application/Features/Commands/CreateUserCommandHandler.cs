using EzPasswordValidator.Validators;
using MediatR;
using System;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using Watchster.Application.Interfaces;
using Watchster.Application.Models;
using Watchster.Domain.Entities;

namespace Watchster.Application.Features.Commands
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserRegistrationResult>
    {
        private readonly IUserRepository repository;
        private readonly ICryptographyService cryptography;

        public CreateUserCommandHandler(IUserRepository repository, ICryptographyService cryptography)
        {
            this.repository = repository;
            this.cryptography = cryptography;
        }

        public async Task<UserRegistrationResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var response = new UserRegistrationResult();

            var isValid = IsValid(request);

            if (!isValid)
            {
                response.ErrorMessage = "Invalid Data";
                return response;
            }

            var user = new User
            {
                Email = request.Email,
                Password = cryptography.GetPasswordSHA3Hash(request.Password),
                IsSubscribed = request.IsSubscribed,
                RegistrationDate = DateTime.Now
            };

            await repository.AddAsync(user);

            response.User = user;

            return response;
        }

        private bool IsValid(CreateUserCommand request)
        {
            return EmailIsValid(request.Email) && PasswordRespectsContraints(request.Password);
        }

        private static bool EmailIsValid(string email)
        {
            try
            {
                var emailAddress = new MailAddress(email);
                return emailAddress.Address == email;
            }
            catch
            {
                return false;
            }
        }
        private static bool PasswordRespectsContraints(string password)
        {
            var validator = new PasswordValidator();
            validator.SetLengthBounds(6, 32);
            return validator.Validate(password);
        }
    }
}
