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
using Watchster.Domain.Entities;

namespace Watchster.Application.UnitTests.Features.Queries
{
    public class GetMoviesFromPageQueryTests
    {
        private readonly GetMoviesFromPageQueryHandler handler;
        private readonly IMovieRepository movieRepository;

        public GetMoviesFromPageQueryTests()
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
            fakeMovieRepository.CallsTo(mRepo => mRepo.GetTotalPages())
                .Returns(1);
            fakeMovieRepository.CallsTo(mRepo => mRepo.GetMoviesFromPage(1))
                .Returns(repoMovies.ToList());
            movieRepository = fakeMovieRepository.FakedObject;
            handler = new GetMoviesFromPageQueryHandler(movieRepository);
        }

        [Test]
        public async Task Given_GetMoviesFromPageQuery_When_GetAllMoviesQueryHandlerIsCalled_Then_GetMoviesFromPageShouldBeCalled()
        {
            var query = new GetMoviesFromPageQuery
            {
                Page = 1
            };
            var response = await handler.Handle(query, default);

            A.CallTo(() => movieRepository.GetMoviesFromPage(1)).MustHaveHappenedOnceExactly();
            response.Should().BeOfType<GetMoviesResponse>();
            Assert.AreEqual(1, response.TotalPages);
            Assert.AreEqual(2, response.Movies.Count);
            foreach (var movie in response.Movies)
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
