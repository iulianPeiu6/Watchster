using Watchster.Jwt.Services.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Watchster.Domain.Entities;

namespace Watchster.Jwt.Services
{
    public class JwtService : IJwtService
    {
        private readonly JwtConfig config;

        public JwtService(IOptions<JwtConfig> config)
        {
            this.config = config.Value;
        }

        public string generateToken(User command)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Email, command.Email),
                new Claim("Password", command.Password),
            };


            var token = new JwtSecurityToken(config.Key,
              config.Issuer,
              claims,
              expires: DateTime.Now.AddMinutes(1440),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
