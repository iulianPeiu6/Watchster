using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Watchster.Aplication.Interfaces;
using Watchster.Application.Interfaces;
using Watchster.DataAccess.Context;
using Watchster.DataAccess.Repositories;

namespace Watchster.DataAccess
{
    public static class DataAccessServiceCollectionExtensions
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<WatchsterContext>(
                options => options.UseSqlServer(configuration.GetConnectionString("WatchsterDB")));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IAppSettingsRepository, AppSettingsRepository>();
            services.AddScoped<IMovieRepository, MovieRepository>();
            return services;
        }
    }
}
