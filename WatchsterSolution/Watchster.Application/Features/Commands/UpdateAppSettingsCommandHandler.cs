using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Watchster.Application.Interfaces;
using Watchster.Domain.Entities;

namespace Watchster.Application.Features.Commands
{
    public class UpdateAppSettingsCommandHandler : IRequestHandler<UpdateAppSettingsCommand, AppSettings>
    {
        private readonly IAppSettingsRepository repository;

        public UpdateAppSettingsCommandHandler(IAppSettingsRepository repository)
        {
            this.repository = repository;
        }

        public Task<AppSettings> Handle(UpdateAppSettingsCommand request, CancellationToken cancellationToken)
        {
            var appSettings = new AppSettings
            {
                Id = request.Id, 
                Section = request.Section,
                Parameter = request.Parameter,
                Description = request.Description,
                Value = request.Value
            };

            return repository.UpdateAsync(appSettings);
        }
    }
}
