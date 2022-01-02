using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Watchster.WebApi.UnitTests.Fakes
{
    public class FakeStartup : Startup
    {
        public FakeStartup(IConfiguration configuration) : base(configuration)
        {
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            //base.ConfigureServices(services);
            //services.AddControllers();
            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Watchster.WebApi", Version = "v1" });
            //});
        }
    }
}
