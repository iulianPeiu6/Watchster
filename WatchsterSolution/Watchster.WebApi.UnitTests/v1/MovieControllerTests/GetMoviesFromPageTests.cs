using FakeItEasy;
using NUnit.Framework;
using Watchster.Application.Features.Queries;
using Watchster.WebApi.UnitTests.v1.Abstracts;

namespace Watchster.WebApi.UnitTests.GetMoviesFromPage
{
    public class GetMoviesFromPageTests : MovieControllerTestsBase
    {
        public GetMoviesFromPageTests() : base()
        {
        }

        [Test]
        public void Given_MovieController_When_GetMoviesFromPageIsCalled_Then_QueryShouldHaveHappenedOnce()
        {
            var command = new GetMoviesFromPageQuery
            {
                Page = 1
            };
            var result = controller.GetFromPage(command);
            A.CallTo(() => mediator.Send(A<GetMoviesFromPageQuery>._, default)).MustHaveHappenedOnceExactly();
        }
    }
}
