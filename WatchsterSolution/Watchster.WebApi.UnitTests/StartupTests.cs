using FakeItEasy;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System.Collections.Generic;
using Watchster.Application.Interfaces;

namespace Watchster.WebApi.UnitTests
{
    public class StartupTests
    {
        private readonly IConfiguration configuration;
        private readonly IServiceCollection services;
        private readonly Startup startup;

        public StartupTests()
        {
            var fakeConfigurationSection = new Fake<IConfigurationSection>();
            fakeConfigurationSection.CallsTo(c => c["WatchsterDB"]).Returns("TestConnectionString");
            var fakeConfiguration = new Fake<IConfiguration>();
            fakeConfiguration.CallsTo(c => c.GetSection("ConnectionStrings")).Returns(fakeConfigurationSection.FakedObject);
            configuration = fakeConfiguration.FakedObject;
            services = new ServiceCollection();
            startup = new Startup(configuration);
        }

        [Test]
        public void Given_Services_When_ConfigureServicesIsCalled_Should_ConfigureServices()
        {
            //arrange

            //act
            startup.ConfigureServices(services);

            //assert
            var serviceProvider = services.BuildServiceProvider();
            serviceProvider.GetRequiredService<IAppSettingsRepository>().Should().NotBeNull();
            serviceProvider.GetRequiredService<IMovieRepository>().Should().NotBeNull();
            serviceProvider.GetRequiredService<IUserRepository>().Should().NotBeNull();
            serviceProvider.GetRequiredService<IResetPasswordCodeRepository>().Should().NotBeNull();
            serviceProvider.GetRequiredService<IRatingRepository>().Should().NotBeNull();
            serviceProvider.GetRequiredService<IMediator>().Should().NotBeNull();
            serviceProvider.GetRequiredService<ICryptographyService>().Should().NotBeNull();
        }

        [Test]
        public void Given_Startup_When_ConfigureIsCalledInDevelopmentEnvironment_Should_Configure()
        {
            //arrange & act
            var webHostBuilder =
              new WebHostBuilder()
                    .UseEnvironment("Development")
                    .UseStartup<Startup>()
                    .ConfigureAppConfiguration((context, configBuilder) =>
                    {
                        configBuilder.AddInMemoryCollection(
                            new Dictionary<string, string>
                            {
                                ["ConnectionStrings:WatchsterDB"] = "TestConnectionString"
                            });
                    });

            //assert
            var serviceProvider = webHostBuilder.Build().Services;
            serviceProvider.GetRequiredService<IAppSettingsRepository>().Should().NotBeNull();
            serviceProvider.GetRequiredService<IMovieRepository>().Should().NotBeNull();
            serviceProvider.GetRequiredService<IUserRepository>().Should().NotBeNull();
            serviceProvider.GetRequiredService<IResetPasswordCodeRepository>().Should().NotBeNull();
            serviceProvider.GetRequiredService<IRatingRepository>().Should().NotBeNull();
            serviceProvider.GetRequiredService<IMediator>().Should().NotBeNull();
            serviceProvider.GetRequiredService<ICryptographyService>().Should().NotBeNull();
        }

        [Test]
        public void Given_Startup_When_ConfigureIsCalledInProductionEnvironment_Should_Configure()
        {
            //arrange & act
            var webHostBuilder =
              new WebHostBuilder()
                    .UseEnvironment("Production")
                    .UseStartup<Startup>()
                    .ConfigureAppConfiguration((context, configBuilder) =>
                    {
                        configBuilder.AddInMemoryCollection(
                            new Dictionary<string, string>
                            {
                                ["ConnectionStrings:WatchsterDB"] = "TestConnectionString"
                            });
                    });

            //assert
            var serviceProvider = webHostBuilder.Build().Services;
            serviceProvider.GetRequiredService<IAppSettingsRepository>().Should().NotBeNull();
            serviceProvider.GetRequiredService<IMovieRepository>().Should().NotBeNull();
            serviceProvider.GetRequiredService<IUserRepository>().Should().NotBeNull();
            serviceProvider.GetRequiredService<IResetPasswordCodeRepository>().Should().NotBeNull();
            serviceProvider.GetRequiredService<IRatingRepository>().Should().NotBeNull();
            serviceProvider.GetRequiredService<IMediator>().Should().NotBeNull();
            serviceProvider.GetRequiredService<ICryptographyService>().Should().NotBeNull();
        }
    }
}
