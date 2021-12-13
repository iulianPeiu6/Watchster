using Watchster.Application.Interfaces;
using Watchster.DataAccess.Context;
using Watchster.Domain.Entities;

namespace Watchster.DataAccess.Repositories
{
    internal class RatingRepository : Repository<Rating>, IRatingRepository
    {
        public RatingRepository(WatchsterContext context) : base(context)
        {
        }
    }
}
