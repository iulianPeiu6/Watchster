using FakeItEasy;
using Faker;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using Watchster.Application.Features.Queries;
using Watchster.Application.Models;
using Watchster.Application.UnitTests.Fakes;

namespace Watchster.Application.UnitTests.Features.Queries
{
    class GetMovieByIdQueryTests
    {
        private FakeMovieRepository movieRepository;
        private GetMovieByIdQueryHandler getMovieByIdQueryHandler;

        public GetMovieByIdQueryTests()
        {
            movieRepository = A.Fake<FakeMovieRepository>();
            getMovieByIdQueryHandler = new GetMovieByIdQueryHandler(movieRepository);
        }

        [Test]
        public async Task Given_GetMovieByIdQuery_When_InvalidMovieId_Should_FailReturnMovie()
        {
            //arrage
            var command = new GetMovieByIdQuery
            {
                Id = RandomNumber.Next(int.MinValue, -1),
            };

            //act
            var response = await getMovieByIdQueryHandler.Handle(command, default);

            //assert
            response.Should().NotBeNull();
            response.ErrorMessage.Should().Be(Error.MovieNotFound);
        }

        [Test]
        public async Task Given_GetMovieByIdQuery_When_ValidMovie_Should_ReturnMovie()
        {
            //arrage
            var command = new GetMovieByIdQuery
            {
                Id = RandomNumber.Next(1, int.MaxValue),
            };

            //act
            var response = await getMovieByIdQueryHandler.Handle(command, default);

            //assert
            response.Should().NotBeNull();
            response.Movie.Should().NotBeNull();
            response.Movie.Genres.Should().NotBeNull();
            response.Movie.Id.Should().BeGreaterThan(-1);
            response.Movie.Overview.Should().NotBeNull();
            response.Movie.Popularity.Should().BeGreaterThan(-1);
            response.Movie.PosterUrl.Should().NotBeNull();
            response.Movie.ReleaseDate.Should().NotBe(DateTime.Now);
            response.Movie.Title.Should().NotBeNull();
            response.Movie.TMDbVoteAverage.Should().BeInRange(0, 10);
            response.Movie.Id.Should().BeGreaterThan(-1);
        }
    }
}
