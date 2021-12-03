using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Watchster.Application.Interfaces;
using Watchster.Domain.Entities;

namespace Watchster.Application.Features.Queries
{
    public class GetAppSettingsBySectionAndParameterQueryHandler : IRequestHandler<GetAppSettingsBySectionAndParameterQuery, AppSettings>
    {
        private readonly IAppSettingsRepository repository;

        public GetAppSettingsBySectionAndParameterQueryHandler(IAppSettingsRepository repository)
        {
            this.repository = repository;
        }
        public Task<AppSettings> Handle(GetAppSettingsBySectionAndParameterQuery request, CancellationToken cancellationToken)
        {
            var appSettings = repository
                .Query(settings => settings.Section == request.Section && settings.Parameter == request.Parameter)
                .FirstOrDefault();

            if (appSettings == null)
            {
                throw new ArgumentException("The requested settings do not exist");
            }

            return Task.Run(() => appSettings);
        }
    }
}
