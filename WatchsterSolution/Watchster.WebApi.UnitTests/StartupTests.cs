using FakeItEasy;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Watchster.Aplication.Interfaces;
using Watchster.Application.Interfaces;
using Watchster.DataAccess;
using Watchster.SendGrid.Services;
using Watchster.TMDb.Services;

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
    }
}
