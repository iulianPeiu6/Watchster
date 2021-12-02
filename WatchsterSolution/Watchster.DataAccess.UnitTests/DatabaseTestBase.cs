using Microsoft.EntityFrameworkCore;
using System;
using Watchster.DataAccess.Context;

namespace Database.UnitTests
{
    public class DatabaseTestBase : IDisposable
    {
        protected readonly WatchsterContext context;

        public DatabaseTestBase()
        {
            var options = new DbContextOptionsBuilder<WatchsterContext>().UseInMemoryDatabase("TestDatabase").Options;
            context = new WatchsterContext(options);
            context.Database.EnsureCreated();
            DatabaseInitializer.Initialize(context);
        }
        public void Dispose()
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}