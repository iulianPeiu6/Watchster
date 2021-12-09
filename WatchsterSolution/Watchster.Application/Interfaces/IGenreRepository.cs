using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Watchster.Aplication.Interfaces;
using Watchster.Domain.Entities;

namespace Watchster.Application.Interfaces
{
    public interface IGenreRepository : IRepository<Genre>
    {
        Task<IList<Genre>> GetGenresByMovieId(Guid movieId);
    }
}
