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
            if(context.Movies.Any() && context.Users.Any() && context.Genres.Any() && context.Ratings.Any() && context.AppSettings.Any())
            {
                return;
            }
            Seed(context);
        }

        private static void Seed(WatchsterContext context)
        {

            var rating = new Rating
            {
                Id = Guid.Parse("eeb15cc4-11c5-4897-a11d-2beca00abb81"),
                UserId = Guid.Parse("56db7a89-9b32-4f36-ba80-b3ac182cef53"),
                MovieId = Guid.Parse("121fdd7c-6671-4db9-b631-5145667287d8"),
                RatingValue = 8.9
            };
            List<Rating> User1Ratings = new List<Rating>();
            User1Ratings.Add(rating);
            var user = new User
            {
                Id = Guid.Parse("56db7a89-9b32-4f36-ba80-b3ac182cef53"),
                Email = "UserTestEmail@yahoo.com",
                Password = "TestPassword",
                IsSubscribed = true,
                RegistrationDate = new DateTime(2021,12,1),
                UserRatings = User1Ratings
            };
            var genres = new[]
            {
                new Genre
                {
                    Id = Guid.Parse("9f05681f-80a7-4d69-8e0a-3dcf65a87919"),
                    TMDbId = 1,
                    Name = "Action"
                },
                new Genre
                {
                    Id = Guid.Parse("0dc9ed89-6ff9-4cce-a94d-68557b178893"),
                    TMDbId = 2,
                    Name = "Comedy"
                }
            };
            List<Genre> Movie1Genres = new List<Genre>();
            Movie1Genres.Add(genres[0]);
            List<Genre> Movie2Genres = new List<Genre>();
            Movie2Genres.Add(genres[0]);
            Movie2Genres.Add(genres[1]);
            var movies = new[]
            {
                new Movie
                {
                    TMDbId = 1,
                    Id = Guid.Parse("1e8a1085-1b1f-4c7c-b630-7086732f7ffc"),
                    Title = "Action Movie",
                    ReleaseDate = new DateTime(2009,5,5),
                    Genres = Movie1Genres,
                    Overview = "This is a movie for tests, it's genre is only Action"
                },
                new Movie
                {
                    TMDbId = 2,
                    Id = Guid.Parse("121fdd7c-6671-4db9-b631-5145667287d8"),
                    Title = "Action-Comedy Movie",
                    ReleaseDate = new DateTime(2021,10,4),
                    Genres = Movie2Genres,
                    Overview = "This is a movie for tests, it's genre is Action and Comedy"
                }
            };
            var appSettings = new AppSettings()
            {
                Id = Guid.Parse("68059ff3-d99e-43b4-9be9-699f492f1a8a"),
                Section = "Test Section",
                Parameter = "Test Parameter",
                Description = "Test Description",
                Value = "Test Value"
            };
            context.AppSettings.Add(appSettings);
            context.Movies.AddRange(movies);
            context.Users.Add(user);
            context.Ratings.Add(rating);
            context.Genres.AddRange(genres);
            context.SaveChanges();
        }
        
    }
}
