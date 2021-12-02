using Watchster.Jwt.Services;
using Watchster.Jwt.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IO;
using System.Reflection;
using System.Text;
using System;

namespace Watchster.Jwt
{
    public static class JwtServiceCollectionExtension
    {
        public static IServiceCollection AddJwt(this IServiceCollection services)
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            var configuration = new ConfigurationBuilder()
                .SetBasePath(path)
                .AddJsonFile("jwtsettings.json", false, reloadOnChange: true)
                .AddUserSecrets<JwtConfig>(true)
                .Build();

            services.Configure<JwtConfig>(options => configuration.Bind(options));
            services.AddTransient<IJwtService, JwtService>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("Key"))),
                    ValidateIssuer = true,
                    ValidateAudience = false
                };
                
            });

            return services;
        }
    }
}
