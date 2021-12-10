using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Watchster.Application.Authentication.Models;
using Watchster.Application.Interfaces;
using Watchster.Application.Models;
using Watchster.Jwt;

namespace Watchster.Application.Features.Commands
{
    public class AuthenticateUserCommandHandler : IRequestHandler<AuthenticateUserCommand, UserAuthenticationResult>
    {
        private readonly IUserRepository repository;
        private readonly ICryptographyService cryptography;
        private readonly AuthenticationConfig config;

        public AuthenticateUserCommandHandler(
            IUserRepository repository,
            ICryptographyService cryptography,
            IOptions<AuthenticationConfig> config)
        {
            this.repository = repository;
            this.cryptography = cryptography;
            this.config = config.Value;
        }

        public Task<UserAuthenticationResult> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                var hashedPassword = cryptography.GetPasswordSHA3Hash(request.Password);

                var user = repository.Query(user => user.Email == request.Email && user.Password == hashedPassword)
                    .FirstOrDefault();

                var response = new UserAuthenticationResult();
                if (user is null)
                {
                    response.ErrorMessage = Error.WrongEmailOrPass;
                    return response;
                }

                response.User = new UserDetails
                {
                    Id = user.Id,
                    Email = user.Email,
                    IsSubscribed = user.IsSubscribed,
                    RegistrationDate = user.RegistrationDate
                };

                response.JwtToken = GenerateToken(request.Email, request.Password);
                return response;
            });
        }

        private string GenerateToken(string email, string password)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim("Password", password)
            };


            var token = new JwtSecurityToken(config.Key,
              config.Issuer,
              claims,
              expires: DateTime.Now.AddMinutes(config.MinutesExpiration),
              signingCredentials: credentials);

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            return jwtToken;
        }
    }
}
