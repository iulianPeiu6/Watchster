using System;
using System.Collections.Generic;
using System.Linq;
using Watchster.DataAccess.Context;
using Watchster.Domain.Entities;

namespace Database.UnitTests
{
    public class DatabaseInitializer
    {
        public static void Initialize(WatchsterContext context)
        {
            if (context.Movies.Any() && context.Users.Any() && context.Ratings.Any() && context.AppSettings.Any())
            {
                return;
            }
            Seed(context);
        }

        private static void Seed(WatchsterContext context)
        {

            var rating = new Rating
            {
                Id = 1,
                UserId = 1,
                MovieId = 1,
                RatingValue = 8.9
            };
            List<Rating> User1Ratings = new List<Rating>();
            User1Ratings.Add(rating);
            var user = new User
            {
                Id = 1,
                Email = "UserTestEmail@yahoo.com",
                Password = "TestPassword",
                IsSubscribed = true,
                RegistrationDate = new DateTime(2021, 12, 1),
                UserRatings = User1Ratings
            };
            var movies = new[]
            {
                new Movie
                {
                    TMDbId = 1,
                    Id = 1,
                    Title = "Action Movie",
                    ReleaseDate = new DateTime(2009,5,5),
                    Genres = "Crime, Action",
                    Overview = "This is a movie for tests, it's genre is only Action"
                },
                new Movie
                {
                    TMDbId = 2,
                    Id = 2,
                    Title = "Action-Comedy Movie",
                    ReleaseDate = new DateTime(2021,10,4),
                    Genres = "Crime, Action",
                    Overview = "This is a movie for tests, it's genre is Action and Comedy"
                }
            };
            var appSettings = new AppSettings()
            {
                Id = 1,
                Section = "Test Section",
                Parameter = "Test Parameter",
                Description = "Test Description",
                Value = "Test Value"
            };
            context.AppSettings.Add(appSettings);
            context.Movies.AddRange(movies);
            context.Users.Add(user);
            context.Ratings.Add(rating);
            context.SaveChanges();
        }

    }
}
