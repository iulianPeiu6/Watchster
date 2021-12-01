using Watchster.Application.Interfaces;
using Watchster.DataAccess.Context;
using Watchster.Domain.Entities;

namespace Watchster.DataAccess.Repositories
{
    public class AppSettingsRepository : Repository<AppSettings>, IAppSettingsRepository
    {
        public AppSettingsRepository(WatchsterContext watchsterContext) : base(watchsterContext)
        {
        }
    }
}
