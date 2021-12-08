using Microsoft.EntityFrameworkCore;
using Watchster.Domain.Entities;

namespace Watchster.DataAccess.Context
{
    public sealed class WatchsterContext : DbContext
    {
        public DbSet<AppSettings> AppSettings { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<Rating> Ratings { get; set; }

        public DbSet<ResetPasswordCode> ResetPasswordCodes { get; set; }

        public WatchsterContext(DbContextOptions<WatchsterContext> dbContext) : base(dbContext)
        {

        }
    }
}
