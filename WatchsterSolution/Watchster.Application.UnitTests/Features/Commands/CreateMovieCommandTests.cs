using FakeItEasy;
using Faker;
using FluentAssertions;
using NUnit.Framework;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Threading.Tasks;
using Watchster.Application.Features.Commands;
using Watchster.Application.Interfaces;
using Watchster.Domain.Entities;

namespace Watchster.Application.UnitTests.Features.Commands
{
    public class CreateMovieCommandTests
    {
        private readonly CreateMovieCommandHandler handler;
        private readonly IMovieRepository movieRepository;

        public CreateMovieCommandTests()
        {
            movieRepository = A.Fake<IMovieRepository>();
            handler = new CreateMovieCommandHandler(movieRepository);
        }

        [Test]
        public async Task Given_CreateMovieCommand_When_HandlerIsCalled_Should_CallAddAsyncMethodAsync()
        {
            //arange
            var command = new CreateMovieCommand
            {
                Title = Lorem.Sentence(),
                Overview = Lorem.Sentence(20),
                TMDbId = RandomNumber.Next(),
                ReleaseDate = DateTime.Now,
                Genres = Lorem.Sentences(RandomNumber.Next(3)),
                PosterUrl = Internet.Url(),
                Popularity = RandomNumber.Next(),
                TMDbVoteAverage = RandomNumber.Next(0,10)
            };

            //act
            var response = await handler.Handle(command, default);

            //assert
            A.CallTo(() => movieRepository.AddAsync(A<Movie>._)).MustHaveHappenedOnceExactly();
            response.Should().BeOfType(typeof(int));
        }
    }
}
