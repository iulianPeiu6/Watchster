using FakeItEasy;
using NUnit.Framework;
using System.Threading.Tasks;
using Watchster.Application.Features.Queries;
using Watchster.Application.Interfaces;

namespace Watchster.Application.UnitTests.Features.Queries
{
    public class GetAllRatingsQueryTests
    {
        private readonly GetAllRatingsQueryHandler handler;
        private readonly IRatingRepository ratingRepository;

        public GetAllRatingsQueryTests()
        {
            ratingRepository = A.Fake<IRatingRepository>();
            handler = new GetAllRatingsQueryHandler(ratingRepository);
        }

        [Test]
        public async Task Given_GetAllRatingsQuery_When_GetAllRatingsQueryHandlerIsCalled_Should_GetAllAsyncIsCalledAsync()
        {
            var query = new GetAllRatingsQuery();

            var response = await handler.Handle(query, default);

            A.CallTo(() => ratingRepository.GetAllAsync()).MustHaveHappenedOnceExactly();
        }
    }
}
