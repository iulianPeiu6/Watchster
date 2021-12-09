using System.Collections.Generic;
using System.Threading.Tasks;
using Watchster.Aplication.Interfaces;
using Watchster.Domain.Entities;

namespace Watchster.Application.Interfaces
{
    public interface IMovieRepository : IRepository<Movie>
    {
        Task<IList<Movie>> GetMoviesFromPage(int page);

        Task<int> GetTotalPages();
    }
}
