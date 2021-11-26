using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Reflection;
using Watchster.TMDb.Services;

namespace Watchster.TMDb
{
    public static class TMDbServiceCollectionExtensions
    {
        public static IServiceCollection AddTMDb(this IServiceCollection services)
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            var configuration = new ConfigurationBuilder()
                .SetBasePath(path)
                .AddJsonFile("tmdbsettings.json", false, reloadOnChange: true)
                .AddJsonFile("tmdbsettings.Development.json", true, reloadOnChange: true)
                .AddUserSecrets<TMDbConfig>(true)
                .Build();

            services.Configure<TMDbConfig>(options => configuration.Bind(options));
            services.AddTransient<ITMDbMovieDiscoverService, TMDbMovieDiscoverService>();

            return services;
        }
    }
}
