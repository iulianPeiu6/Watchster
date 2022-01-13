using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Watchster.MovieImporter.Extensions;

namespace Watchster.MovieImporter
{
    static class Program
    {
        private const string EnvironmentKey = "DOTNET_ENVIRONMENT";
        private const string DevelopmentEnvironment = "Development";

        private static readonly string environment = Environment.GetEnvironmentVariable(EnvironmentKey) ?? DevelopmentEnvironment;

        static async Task Main()
        {
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            IHost host = CreateHostBuilder();
            await host.RunAsync();
        }

        private static IHost CreateHostBuilder()
        {
            var builder = new ConfigurationBuilder();
            ConfigurationSetup(builder);
            IConfigurationRoot configuration = builder.Build();

            IHost host = Host.CreateDefaultBuilder()
                .UseEnvironment(environment)
                .ConfigureServices((context, services) =>
                {
                    services.AddMovieImporter(configuration);
                    services.AddLogging(builder =>
                    {
                        builder.AddConfiguration(configuration.GetSection("Logging"));
                        builder.AddFile(o => o.RootPath = AppContext.BaseDirectory);
                    });

                })
                .UseWindowsService()
                .Build();

            return host;
        }

        private static void ConfigurationSetup(IConfigurationBuilder builder)
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            builder.SetBasePath(path)
                .AddJsonFile("movieimportersettings.json", false, reloadOnChange: true)
                .AddJsonFile("movieimportersettings.Development.json", true, reloadOnChange: true)
                .Build();
        }
    }
}
