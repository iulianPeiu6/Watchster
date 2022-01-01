using FakeItEasy;
using NUnit.Framework;
using System.Threading.Tasks;
using Watchster.Application.Features.Queries;
using Watchster.Application.Interfaces;

namespace Watchster.Application.UnitTests.Features.Queries
{
    public class GetAllMoviesQueryTests
    {
        private readonly GetAllMoviesQueryHandler handler;
        private readonly IMovieRepository movieRepository;

        public GetAllMoviesQueryTests()
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
