using FakeItEasy;
using NUnit.Framework;
using Watchster.Application.Features.Queries;
using Watchster.WebApi.UnitTests.v1.Abstracts;

namespace Watchster.WebApi.UnitTests.v1.MovieControllerTests
{
    public class GetAllMoviesTests : MovieControllerTestsBase
    {
        public GetAllMoviesTests() : base()
        {
        }

        [Test]
        public void Given_MovieController_When_GetMoviesFromPageIsCalled_Then_QueryShouldHaveHappenedOnce()
        {
            //arrange
            Fake.ClearRecordedCalls(mediator);

            //act
            var result = controller.GetAllAsync();

            //assert
            A.CallTo(() => mediator.Send(A<GetAllMoviesQuery>._, default)).MustHaveHappenedOnceExactly();
        }
    }
}
