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
using Watchster.Domain.Entities;

namespace Watchster.Application.UnitTests.Features.Queries
{
    public class GetAllMoviesQueryTests
    {
        private readonly GetAllMoviesQueryHandler handler;
        private readonly IMovieRepository movieRepository;

        public GetAllMoviesQueryTests()
        {
            var fakeMovieRepository = new Fake<IMovieRepository>();

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
                    Id = 1,
                    Title = Lorem.Sentence(),
                    Overview = Lorem.Sentence(),
                    Genres = Lorem.Sentence(),
                    Popularity = RandomNumber.Next(),
                    PosterUrl = Internet.Url(),
                    ReleaseDate = DateTime.Now,
                    TMDbId = RandomNumber.Next(),
                    TMDbVoteAverage = RandomNumber.Next()
                }
            }.AsEnumerable();

            fakeMovieRepository.CallsTo(mRepo => mRepo.GetAllAsync())
                .Returns(Task.FromResult(repoMovies));

            movieRepository = fakeMovieRepository.FakedObject;
            handler = new GetAllMoviesQueryHandler(movieRepository);
        }

        [Test]
        public async Task Given_GetAllMoviesQuery_When_GetAllMoviesQueryHandlerIsCalled_Should_GetAllAsyncIsCalledAsync()
        {
            var query = new GetAllMoviesQuery();

            var response = await handler.Handle(query, default);

            A.CallTo(() => movieRepository.GetAllAsync()).MustHaveHappenedOnceExactly();
            response.Should().NotBeNullOrEmpty();
            response.Count().Should().Be(2);
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
