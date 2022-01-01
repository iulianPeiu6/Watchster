using FakeItEasy;
using Faker;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Watchster.Application.Features.Queries;
using Watchster.Application.Interfaces;
using Watchster.Application.Models;
using Watchster.Application.Utils.Cryptography;

namespace Watchster.Application.UnitTests.Features.Queries
{
    public class GetAllMoviesTests
    {
        private readonly GetAllMoviesQueryHandler handler;
        private readonly IMovieRepository movieRepository;

        public GetAllMoviesTests()
        {
            movieRepository = A.Fake<IMovieRepository>();
            handler = new GetAllMoviesQueryHandler(movieRepository);
        }

        [Test]
        public async Task Given_GetAllMoviesQuery_When_GetAllMoviesQueryHandlerIsCalled_Should_GetAllAsyncIsCalledAsync()
        {
            var query = new GetAllMoviesQuery();

            var response = await handler.Handle(query, default);

            A.CallTo(() => movieRepository.GetAllAsync()).MustHaveHappenedOnceExactly();
        }
    }
}
