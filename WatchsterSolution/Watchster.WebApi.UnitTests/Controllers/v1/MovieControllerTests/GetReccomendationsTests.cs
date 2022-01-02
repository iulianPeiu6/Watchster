using FakeItEasy;
using NUnit.Framework;
using Watchster.Application.Features.Queries;
using Watchster.WebApi.UnitTests.v1.Abstracts;

namespace Watchster.WebApi.UnitTests.v1.MovieControllerTests
{
    public class GetReccomendationsTests : MovieControllerTestsBase
    {
        public GetReccomendationsTests() : base()
        {
        }

        [Test]
        public void Given_MovieController_When_GetReccomendationsIsCalled_Then_GetReccomendationsQueryIsCalled()
        {
            //arrange
            var userId = ValidUserId;

            //act
            var result = controller.GetRecommendationsAsync(userId);

            //assert
            A.CallTo(() => mediator.Send(A<GetReccomendationsQuery>._, default)).MustHaveHappenedOnceExactly();
        }
    }
}
