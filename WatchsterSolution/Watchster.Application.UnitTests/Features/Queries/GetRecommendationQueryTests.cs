using FakeItEasy;
using Faker;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Watchster.Application.Features.Queries;
using Watchster.Application.Interfaces;
using Watchster.Application.Models;
using Watchster.Application.Utils.ML.Models;
using Watchster.Domain.Entities;

namespace Watchster.Application.UnitTests.Features.Queries
{
    public class GetRecommendationsQueryTests
    {
        /*
        private readonly GetRecommendationsQueryHandler handler;
        private readonly IMovieRepository movieRepository;
        private readonly IRatingRepository ratingRepository;
        private readonly IMovieRecommender movieRecommender;

        public GetRecommendationsQueryTests()
        {   
            var fakeRatingRepository = new Fake<IRatingRepository>();
            var movieIds = new List<int>
            {
                1,
                2,
                3
            };
            
            var repoRatings = new List<Rating>
            {
                new Rating
                {
                    Id = 1,
                    UserId = 1,
                    MovieId = 1,
                    RatingValue = 7.2
                },
                new Rating
                {
                    Id = 2,
                    UserId = 1,
                    MovieId = 2,
                    RatingValue = 8.2
                },
                new Rating
                {
                    Id = 3,
                    UserId = 2,
                    MovieId = 2,
                    RatingValue = 9.2
                },
                new Rating
                {
                    Id = 4,
                    UserId = 2,
                    MovieId = 3,
                    RatingValue = 9.5
                }
            }.AsEnumerable();

            // Aici se produce exceptia :  The target of this call is not the fake object being configured.
            
            fakeRatingRepository.CallsTo(rRepo => rRepo.Query().Select(rating => rating.MovieId).Distinct().ToList())
                .Returns(movieIds);
            fakeRatingRepository.CallsTo(rRepo => rRepo.GetByIdAsync(1))
                .Returns(repoRatings.ToList()[0]);
            fakeRatingRepository.CallsTo(rRepo => rRepo.GetByIdAsync(2))
                .Returns(repoRatings.ToList()[2]);

            var repoMovies = new List<Movie>
            {
                new Movie
                {
                    Id = 1,
                    Title = Lorem.Sentence(),
                    Overview = Lorem.Sentence(),
                    Genres = Lorem.Sentence(),
                    Popularity = RandomNumber.Next(),
                    PosterUrl = Internet.Url(),
                    ReleaseDate = DateTime.Now,
                    TMDbId = RandomNumber.Next(),
                    TMDbVoteAverage = RandomNumber.Next()
                },
                new Movie
                {
                    Id = 2,
                    Title = Lorem.Sentence(),
                    Overview = Lorem.Sentence(),
                    Genres = Lorem.Sentence(),
                    Popularity = RandomNumber.Next(),
                    PosterUrl = Internet.Url(),
                    ReleaseDate = DateTime.Now,
                    TMDbId = RandomNumber.Next(),
                    TMDbVoteAverage = RandomNumber.Next()
                },
                new Movie
                {
                    Id = 3,
                    Title = Lorem.Sentence(),
                    Overview = Lorem.Sentence(),
                    Genres = Lorem.Sentence(),
                    Popularity = RandomNumber.Next(),
                    PosterUrl = Internet.Url(),
                    ReleaseDate = DateTime.Now,
                    TMDbId = RandomNumber.Next(),
                    TMDbVoteAverage = RandomNumber.Next()
                },
            }.AsEnumerable();

            var fakeMovieRepository = new Fake<IMovieRepository>();
            fakeMovieRepository.CallsTo(mRepo => mRepo.GetByIdAsync(1))
                .Returns(repoMovies.ToList()[0]);
            fakeMovieRepository.CallsTo(mRepo => mRepo.GetByIdAsync(2))
                .Returns(repoMovies.ToList()[1]);
            fakeMovieRepository.CallsTo(mRepo => mRepo.GetByIdAsync(3))
                .Returns(repoMovies.ToList()[2]);

            movieRepository = fakeMovieRepository.FakedObject;
            ratingRepository = fakeRatingRepository.FakedObject;
            movieRecommender = A.Fake<IMovieRecommender>();
            handler = new GetRecommendationsQueryHandler(ratingRepository, movieRepository, movieRecommender);
        }

        [Test]
        public async Task Given_GetRecommendationsQuery_When_GetRecommendationsQueryHandlerIsCalled_Then_ShouldReceiveAGetRecommendationsResponse()
        {
            var query = new GetRecommendationsQuery
            {
               UserId = 1 
            };
            var movieRating = new MovieRating
            {
                UserId = 1,
                MovieId = 1
            };
            var response = await handler.Handle(query, default);
            response.Should().BeOfType<GetRecommendationsResponse>();
        }*/
    }
}
