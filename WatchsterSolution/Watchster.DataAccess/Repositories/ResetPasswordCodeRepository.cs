
using Watchster.Application.Interfaces;
using Watchster.DataAccess.Context;
using Watchster.Domain.Entities;

namespace Watchster.DataAccess.Repositories
{
    internal class ResetPasswordCodeRepository : Repository<ResetPasswordCode>, IResetPasswordCodeRepository
    {
        public ResetPasswordCodeRepository(WatchsterContext context) : base(context)
        {
        }
    }
}
