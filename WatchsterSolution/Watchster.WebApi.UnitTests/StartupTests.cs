using FakeItEasy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Watchster.WebApi.UnitTests.Fakes;

namespace Watchster.WebApi.UnitTests
{
    public class StartupTests
    {
        private readonly IConfiguration configuration;
        private readonly IServiceCollection services;
        private readonly FakeStartup startup;

        public StartupTests()
        {
            configuration = A.Fake<IConfiguration>();
            services = A.Fake<IServiceCollection>();
            startup = new FakeStartup(configuration);
        }

        [Test]
        public void Given_Services_When_ConfigureServicesIsCalled_Should_ConfigureServices()
        {
            //arrange
            Fake.ClearRecordedCalls(services);

            //act
            startup.ConfigureServices(services);

            //assert
            //A.CallTo(() => services.AddControllers()).MustHaveHappened();
            //A.CallTo(() => services.AddSwaggerGen(A<Action<SwaggerGenOptions>>._)).MustHaveHappened();
        }
    }
}
