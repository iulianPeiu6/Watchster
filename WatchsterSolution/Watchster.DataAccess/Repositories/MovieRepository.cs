using Watchster.Application.Interfaces;
using Watchster.DataAccess.Context;
using Watchster.Domain.Entities;

namespace Watchster.DataAccess.Repositories
{
    internal class MovieRepository : Repository<Movie>, IMovieRepository
    {
        public MovieRepository(WatchsterContext context) : base(context)
        {
        }
    }
}
