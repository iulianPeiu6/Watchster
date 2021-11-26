using Microsoft.Extensions.DependencyInjection;
using Watchster.MLUtil.Services;
using Watchster.MLUtil.Services.Interfaces;

namespace Watchster.MLUtil
{
    public static class MLUtilServiceCollectionExtensions
    {
        public static IServiceCollection AddMLUtil(this IServiceCollection services)
        {
            services.AddTransient<IMovieRecommender, MovieRecommender>();
            return services;
        }
    }
}
