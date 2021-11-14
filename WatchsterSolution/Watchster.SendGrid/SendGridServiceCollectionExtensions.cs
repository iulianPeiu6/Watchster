using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Reflection;
using Watchster.SendGrid.Services;

namespace Watchster.SendGrid
{
    public static class SendGridServiceCollectionExtensions
    {
        public static IServiceCollection AddSendGrid(this IServiceCollection services)
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            var configuration = new ConfigurationBuilder()
                .SetBasePath(path)
                .AddJsonFile("sendgridsettings.json", false, reloadOnChange: true)
                .AddJsonFile("sendgridsettings.Development.json", true, reloadOnChange: true)
                .AddUserSecrets<SendGridConfig>(true)
                .Build();

            services.Configure<SendGridConfig>(options => configuration.Bind(options));
            services.AddTransient<ISendGridService, SendGridService>();

            return services;
        }
    }
}
