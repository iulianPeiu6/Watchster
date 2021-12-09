using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Watchster.Application.Interfaces;
using Watchster.DataAccess.Context;
using Watchster.Domain.Entities;

namespace Watchster.DataAccess.Repositories
{
    public class GenreRepository : Repository<Genre>, IGenreRepository
    {
        public GenreRepository(WatchsterContext context) : base(context)
        {
        }
        public async Task<IList<Genre>> GetGenresByMovieId(Guid movieId)
        {
            List<Genre> genres = context.Set<Genre>().Where(genre => genre.MovieId == movieId).ToList();

            return genres;
        }
    }
}
