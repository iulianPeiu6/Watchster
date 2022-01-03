using Microsoft.Extensions.DependencyInjection;
using Watchster.Application.Interfaces;

namespace Watchster.Application.Utils.ML
{
    public static class MLUtilServiceCollectionExtensions
    {
        public static IServiceCollection AddMLUtil(this IServiceCollection services)
        {
            services.AddTransient<IMovieRecommender, MovieRecommender>();
            services.AddTransient<IMLModelBuilder, MLModelBuilder>();
            return services;
        }
    }
}
