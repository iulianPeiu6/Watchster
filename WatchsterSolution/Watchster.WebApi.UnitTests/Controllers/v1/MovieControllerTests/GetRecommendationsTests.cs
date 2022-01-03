using FakeItEasy;
using NUnit.Framework;
using Watchster.Application.Features.Queries;
using Watchster.WebApi.UnitTests.v1.Abstracts;

namespace Watchster.WebApi.UnitTests.v1.MovieControllerTests
{
    public class GetRecommendationsTests : MovieControllerTestsBase
    {
        public GetRecommendationsTests() : base()
        {
        }

        [Test]
        public void Given_MovieController_When_GetRecommendationsIsCalled_Then_GetRecommendationsQueryIsCalled()
        {
            //arrange
            var userId = ValidUserId;

            //act
            var result = controller.GetRecommendationsAsync(userId);

            //assert
            A.CallTo(() => mediator.Send(A<GetRecommendationsQuery>._, default)).MustHaveHappenedOnceExactly();
        }
    }
}
