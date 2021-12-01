using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Watchster.Application.Interfaces;
using Watchster.MLUtil;
using Watchster.SendGrid;
using Watchster.TMDb;

namespace Watchster.Application
{
    public static class ApplicationServiceCollectionExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddSendGrid();
            services.AddTMDb();
            services.AddMLUtil();
        }
    }
}
