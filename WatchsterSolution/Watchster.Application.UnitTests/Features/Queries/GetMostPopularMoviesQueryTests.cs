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
using Watchster.Application.UnitTests.Fakes;
using Watchster.Domain.Entities;

namespace Watchster.Application.UnitTests.Features.Queries
{
    class GetMostPopularMoviesQueryTests
    {
        private readonly GetMostPopularMoviesQueryHandler handler;
        private readonly IMovieRepository movieRepository;

        public GetMostPopularMoviesQueryTests()
        {
            movieRepository = A.Fake<FakeMovieRepository>();
            handler = new GetMostPopularMoviesQueryHandler(movieRepository);
        }

        [Test]
        public async Task Given_GetMostPopularMoviesQuery_When_GetMostPopularMoviesQueryHandlerIsCalled_Then_GetMostPopularMoviesQueryHandlerShouldReturnResponse()
        {
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

            foreach (Movie movie in repoMovies)
            {
                await movieRepository.AddAsync(movie);
            }

            var query = new GetMostPopularMoviesQuery();

            var response = await handler.Handle(query, default);

            response.Should().NotBeNullOrEmpty();
            response.Count().Should().Be(3);
            foreach (var movie in response)
            {
                movie.Should().NotBeNull();
                movie.TMDbId.Should().BePositive();
                movie.Title.Should().NotBeNullOrEmpty();
                movie.ReleaseDate.Should().NotBeNull();
                movie.Genres.Should().NotBeNull();
                movie.Overview.Should().NotBeNullOrEmpty();
            }
        }
    }
}
