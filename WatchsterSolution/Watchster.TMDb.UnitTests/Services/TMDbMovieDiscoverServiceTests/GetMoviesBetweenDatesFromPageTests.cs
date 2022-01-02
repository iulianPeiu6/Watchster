using FakeItEasy;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using System;
using TMDbLib.Objects.Exceptions;
using Watchster.TMDb.Services;

namespace Watchster.TMDb.UnitTests.Services.TMDbMovieDiscoverServiceTests
{
    public class GetMoviesBetweenDatesFromPageTests
    {
        private readonly ITMDbMovieDiscoverService tmdbMovieDiscoverService;
        private readonly ILogger<TMDbMovieDiscoverService> logger;
        private readonly IOptions<TMDbConfig> config;

        public GetMoviesBetweenDatesFromPageTests()
        {
            logger = A.Fake<ILogger<TMDbMovieDiscoverService>>();
            config = Options.Create(
                new TMDbConfig
                {
                    ApiKey = SecurityHandler.DecryptStringFromBytes(
                        SecurityHandler.EncryptedTMDbApiKey,
                        SecurityHandler.DefaultKey,
                        SecurityHandler.DefaultIV)
                });
            tmdbMovieDiscoverService = new TMDbMovieDiscoverService(logger, config);
        }

        [Test]
        public void Given_FromToDates_When_GetMoviesBetweenDatesFromPageIsCalledWithNoPage_Then_ShouldReturnMoviesFromFirstPage()
        {
            //arrange
            var from = new DateTime(1900, 12, 12);
            var to = new DateTime(2000, 10, 24);

            //act
            var movies = tmdbMovieDiscoverService.GetMoviesBetweenDatesFromPage(from, to);

            //assert
            movies.Should().NotBeNull();
            movies.TotalPages.Should().BePositive();
            movies.Movies.Should().NotBeNullOrEmpty();
            foreach (var movie in movies.Movies)
            {
                movie.Should().NotBeNull();
                movie.TMDbId.Should().BePositive();
                movie.Title.Should().NotBeNullOrEmpty();
                movie.ReleaseDate.Should().NotBeNull();
                movie.ReleaseDate.Should().BeAfter(from);
                movie.ReleaseDate.Should().BeBefore(to);
                movie.Genres.Should().NotBeNull();

                foreach (var genre in movie.Genres)
                {
                    genre.Should().NotBeNull();
                    genre.Name.Should().NotBeNullOrEmpty();
                    genre.TMDbId.Should().BePositive();
                }

                movie.Popularity.Should().BePositive();
                movie.Overview.Should().NotBeNullOrEmpty();
                movie.PosterPath.Should().NotBeNullOrEmpty();
                movie.VoteAverage.Should().BeInRange(0, 10);
            }
            //movies.Movies
        }

        [Test]
        public void Given_FromToDates_When_GetMoviesBetweenDatesFromPageIsCalledWithInvalidPage_Then_ShouldThrowGeneralHttpException()
        {
            //arrange
            var from = new DateTime(1900, 12, 12);
            var to = new DateTime(2000, 10, 24);
            var page = int.MaxValue;

            //act
            Action tmdbAPICall = () => tmdbMovieDiscoverService.GetMoviesBetweenDatesFromPage(from, to, page);

            //assert
            tmdbAPICall.Should().Throw<GeneralHttpException>().WithMessage("TMDb returned an unexpected HTTP error");
        }
    }
}
