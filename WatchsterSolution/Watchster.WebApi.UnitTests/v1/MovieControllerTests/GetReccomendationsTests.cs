using FakeItEasy;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using Watchster.Application.Features.Queries;
using Watchster.WebApi.Controllers.v1;
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
            var userId = 1;

            var result = controller.GetRecommendationsAsync(userId);

            A.CallTo(() => mediator.Send(A<GetReccomendationsQuery>._, default)).MustHaveHappenedOnceExactly();
        }
    }
}
