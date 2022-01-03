using FakeItEasy;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.ML;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Watchster.Application.Features.Queries;
using Watchster.Application.Utils.ML;
using Watchster.Application.Utils.ML.Models;
using Watchster.Domain.Entities;

namespace Watchster.Application.UnitTests.Utils.ML
{
    public class MLModelBuilderTests
    {
        protected readonly IMLModelBuilder modelBuilder;
        private readonly IMediator mediator;
        private readonly ILogger<MLModelBuilder> logger;

        public MLModelBuilderTests()
        {
            logger = A.Fake<ILogger<MLModelBuilder>>();
            var fakeMediator = new Fake<IMediator>();
            fakeMediator.CallsTo(m => m.Send(A<GetAllRatingsQuery>._, default))
                .Returns(Task.FromResult(new List<Rating>
                {
                    new Rating()
                    {
                        UserId = 1,
                        MovieId = 1,
                        RatingValue = 10
                    },
                    new Rating()
                    {
                        UserId = 1,
                        MovieId = 2,
                        RatingValue = 10
                    },
                    new Rating()
                    {
                        UserId = 2,
                        MovieId = 1,
                        RatingValue = 10
                    }
                }.AsEnumerable()));

            mediator = fakeMediator.FakedObject;
            modelBuilder = new MLModelBuilder(logger, mediator);
        }

        [SetUp]
        public void SetUp()
        {
            Fake.ClearRecordedCalls(logger);
            Fake.ClearRecordedCalls(mediator);
        }

        [TearDown]
        public void TearDown()
        {
            Fake.ClearRecordedCalls(logger);
            Fake.ClearRecordedCalls(mediator);
        }

        [Test]
        public async Task Given_MLModelBuilder_When_ConstructMoviePredictModelAsyncIsCalled_Then_ModelIsConstructedAccordglyAsync()
        {
            //arrange

            //act
            var predictEngine = await modelBuilder.ConstructMoviePredictModelAsync();

            //assert
            A.CallTo(() => mediator.Send(A<GetAllRatingsQuery>._, default)).MustHaveHappenedOnceExactly();
            predictEngine.Should().NotBeNull();
            predictEngine.Should().BeOfType<PredictionEngine<MovieRating, MovieRatingPrediction>>();
        }
    }
}
