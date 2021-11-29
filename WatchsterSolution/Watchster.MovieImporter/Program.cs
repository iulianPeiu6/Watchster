using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Watchster.MovieImporter.Extensions;
using Watchster.MovieImporter.Settings;
using Watchster.TMDb;

namespace Watchster.MovieImporter
{
    static class Program
    {
        private const string EnvironmentKey = "DOTNET_ENVIRONMENT";
        private const string DevelopmentEnvironment = "Development";
        private const string QuartzSettingsSectionName = "QuartzSettings";

        private static readonly string enviroment = Environment.GetEnvironmentVariable(EnvironmentKey) ?? DevelopmentEnvironment;

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
            var quartzSettings = configuration.GetSection(QuartzSettingsSectionName).Get<QuartzSettings>();

            IHost host = Host.CreateDefaultBuilder()
                .UseEnvironment(enviroment)
                .ConfigureServices((context, services) =>
                {
                    services.AddTMDb();
                    services.AddQuartz(quartzSettings);
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
