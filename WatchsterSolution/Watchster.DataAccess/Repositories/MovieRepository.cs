using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Watchster.Application.Interfaces;
using Watchster.DataAccess.Context;
using Watchster.Domain.Entities;

namespace Watchster.DataAccess.Repositories
{
    public class MovieRepository : Repository<Movie>, IMovieRepository
    {
        private const int MOVIE_PAGE_SIZE = 2000;
        public MovieRepository(WatchsterContext context) : base(context)
        {
        }
        public async Task<IList<Movie>> GetMoviesFromPage(int page)
        {
            int movieTotalPages = await GetTotalPages();
            int skipMovies = (page - 1) * MOVIE_PAGE_SIZE;
            if (page <= 0 || page > movieTotalPages)
            {
                throw new ArgumentException("Invalid Page Value");
            }

            List<Movie> movies = await context.Set<Movie>().AsNoTracking().OrderBy(movie => movie.Id).Skip(skipMovies).Take(MOVIE_PAGE_SIZE).ToListAsync();

            return movies;
        }

        public async Task<int> GetTotalPages()
        {
            List<Movie> movies = await context.Set<Movie>().ToListAsync();
            int TotalMovies = movies.Count;
            return (int)Math.Ceiling(((double)TotalMovies) / MOVIE_PAGE_SIZE);
        }
    }
}
