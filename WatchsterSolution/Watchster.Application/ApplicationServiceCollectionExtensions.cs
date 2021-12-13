using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;
using Watchster.Application.Interfaces;
using Watchster.Application.Utils.Cryptography;
using Watchster.Application.Utils.ML;
using Watchster.Jwt;
using Watchster.SendGrid;
using Watchster.TMDb;

namespace Watchster.Application
{
    public static class ApplicationServiceCollectionExtensions
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient<ICryptographyService, CryptographyService>();
            services.AddSendGrid();
            services.AddTMDb();
            services.AddMLUtil();
            services.AddAuthentication(configuration);
        }

        public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AuthenticationConfig>(options => configuration.GetSection("Authentication").Bind(options));
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
