using FakeItEasy;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Watchster.Application.Features.Queries;
using Watchster.WebApi.UnitTests.v1.Abstracts;

namespace Watchster.WebApi.UnitTests.Controllers.v1.MovieControllerTests
{
    public class GetMostPopularMoviesTests : MovieControllerTestsBase
    {
        public GetMostPopularMoviesTests() : base()
        {
        }

        [Test]
        public void Given_MovieController_When_GetMostPopularMoviesIsCalled_Then_QueryShouldHaveHappenedOnce()
        {
            //arrange

            //act
            var result = controller.GetMostPopular();

            //assert
            A.CallTo(() => mediator.Send(A<GetMostPopularMoviesQuery>._, default)).MustHaveHappenedOnceExactly();
        }
    }
}
