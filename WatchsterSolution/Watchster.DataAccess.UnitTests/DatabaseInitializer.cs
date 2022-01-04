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

            var rating = new[]
            {
                new Rating
                {
                    Id = 1,
                    UserId = 1,
                    MovieId = 1,
                    RatingValue = 8.9
                },
                new Rating
                {
                    Id = 2,
                    UserId = 2,
                    MovieId = 1,
                    RatingValue = 9.0
                }
            };
            List<Rating> User1Ratings = new List<Rating>();
            List<Rating> User2Ratings = new List<Rating>();
            User1Ratings.Add(rating[0]);
            User2Ratings.Add(rating[1]);
            var user = new[]
            {
                new User
                {
                    Id = 1,
                    Email = "UserTestEmail@yahoo.com",
                    Password = "TestPassword",
                    IsSubscribed = true,
                    RegistrationDate = new DateTime(2021, 12, 1),
                    UserRatings = User1Ratings
                },
                new User
                {
                    Id = 2,
                    Email = "UserTestEmail2@yahoo.com",
                    Password = "TestPassword2",
                    IsSubscribed = true,
                    RegistrationDate = new DateTime(2021, 12, 1),
                    UserRatings = User2Ratings
                }
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
            var appSettings = new[]
            {
                new AppSettings()
                {
                    Id = 1,
                    Section = "Test Section",
                    Parameter = "Test Parameter",
                    Description = "Test Description",
                    Value = "Test Value"
                },
                new AppSettings()
                {
                    Id = 2,
                    Section = "Test Section 2",
                    Parameter = "Test Parameter 2",
                    Description = "Test Description 2",
                    Value = "Test Value 2"
                }
            };
            context.AppSettings.AddRange(appSettings);
            context.Movies.AddRange(movies);
            context.Users.AddRange(user);
            context.Ratings.AddRange(rating);
            context.SaveChanges();
        }

    }
}
