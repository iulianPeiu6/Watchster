using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System.Threading.Tasks;
using Watchster.Application.Features.Queries;
using Watchster.WebApi.UnitTests.v1.Abstracts;

namespace Watchster.WebApi.UnitTests.v1.UserControllerTests
{
    public class GetUserTests : UserControllerTestsBase
    {
        public GetUserTests() : base()
        {
        }

        [Test]
        public async Task Given_ValidUserId_When_GetUserIsCalled_Should_ReturnStatus200OkAsync()
        {
            //arrange
            var userId = ValidUserId;
            Fake.ClearRecordedCalls(mediator);

            //act
            var response = await controller.GetUserAsync(userId);

            //assert
            response.Should().BeOfType<OkObjectResult>();
            A.CallTo(() => mediator.Send(A<GetUserDetailsQuery>._, default)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public async Task Given_InvalidUserId_When_GetUserIsCalled_Should_ReturnStatus404NotFoundAsync()
        {
            //arrange
            var userId = InvalidUserId;
            Fake.ClearRecordedCalls(mediator);

            //act
            var response = await controller.GetUserAsync(userId);

            //assert
            A.CallTo(() => mediator.Send(A<GetUserDetailsQuery>._, default)).MustHaveHappenedOnceExactly();
            response.Should().BeOfType<NotFoundObjectResult>();
            response.As<NotFoundObjectResult>().StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }
    }
}
