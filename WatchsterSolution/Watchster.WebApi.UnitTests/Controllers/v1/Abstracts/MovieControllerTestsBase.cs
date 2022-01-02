using FakeItEasy;
using MediatR;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System.Threading.Tasks;
using Watchster.Application.Features.Commands;
using Watchster.Application.Features.Queries;
using Watchster.Application.Models;
using Watchster.WebApi.Controllers.v1;

namespace Watchster.WebApi.UnitTests.v1.Abstracts
{
    public class MovieControllerTestsBase
    {
        protected readonly MovieController controller;
        protected readonly IMediator mediator;
        protected const int ValidMovieId = 3;
        protected const int InvalidMovieId = -1;
        protected const int ValidUserId = 3;
        protected const int InvalidUserId = -1;

        public MovieControllerTestsBase()
        {
            var logger = A.Fake<ILogger<MovieController>>();
            var fakeMediator = new Fake<IMediator>();

            fakeMediator.CallsTo(mediator => mediator.Send(A<GetMovieByIdQuery>.That.Matches(m => m.Id == InvalidMovieId), default))
                .Returns(Task.FromResult(new MovieResult { ErrorMessage = Error.MovieNotFound }));

            fakeMediator.CallsTo(mediator => mediator.Send(A<AddRatingCommand>.That.Matches(m => m.UserId == InvalidUserId), default))
                .Returns(Task.FromResult(new AddRatingResponse { ErrorMessage = Error.UserNotFound, IsSuccess = false }));

            fakeMediator.CallsTo(mediator => mediator.Send(A<AddRatingCommand>.That.Matches(m => m.MovieId == InvalidMovieId), default))
                .Returns(Task.FromResult(new AddRatingResponse { ErrorMessage = Error.MovieNotFound, IsSuccess = false }));

            fakeMediator.CallsTo(mediator => mediator.Send(A<AddRatingCommand>.That.Matches(m => m.Rating < 0 || m.Rating > 10), default))
                .Returns(Task.FromResult(new AddRatingResponse { ErrorMessage = Error.RatingNotInRange, IsSuccess = false }));

            fakeMediator.CallsTo(mediator => mediator.Send(A<AddRatingCommand>.That.Matches(m => m.UserId == 0 && m.MovieId == 0), default))
                .Returns(Task.FromResult(new AddRatingResponse { ErrorMessage = Error.MovieAlreadyRated, IsSuccess = false }));

            mediator = fakeMediator.FakedObject;
            controller = new MovieController(mediator, logger);
        }

        [SetUp]
        public void SetUp()
        {
            Fake.ClearRecordedCalls(mediator);
        }

        [TearDown]
        public void TearDown()
        {
            Fake.ClearRecordedCalls(mediator);
        }
    }
}
