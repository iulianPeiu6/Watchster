using FakeItEasy;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using Watchster.Application.Interfaces;
using Watchster.Application.Utils.ML;
using Watchster.Application.Utils.ML.Models;

namespace Watchster.Application.UnitTests.Utils.ML
{
    public class PredictMovieTests : MLModelBuilderTests
    {
        private readonly IMovieRecommender movieRecommender;
        private readonly ILogger<MovieRecommender> logger;
        private readonly IMLModelBuilder fakeModelBuilder;

        public PredictMovieTests() : base()
        {
            logger = A.Fake<ILogger<MovieRecommender>>();
            fakeModelBuilder = A.Fake<IMLModelBuilder>();
            movieRecommender = new MovieRecommender(logger, fakeModelBuilder);
        }

        [SetUp]
        public void Setup()
        {
            Fake.ClearRecordedCalls(logger);
            Fake.ClearRecordedCalls(fakeModelBuilder);
        }

        [TearDown]
        public void Teardown()
        {
            Fake.ClearRecordedCalls(logger);
            Fake.ClearRecordedCalls(fakeModelBuilder);
        }

        [Test]
        public void Given_MovieRecommender_When_IsConstructed_Then_ConstructMoviePredictModelAsyncShouldBeCalled()
        {
            //arrange

            //act
            var movieRecommender = new MovieRecommender(logger, fakeModelBuilder);

            //assert
            A.CallTo(() => fakeModelBuilder.ConstructMoviePredictModelAsync()).MustHaveHappenedOnceExactly();
        }

        [Test]
        public void Given_Movie_When_IsPredictedWithNullPredictEngine_Then_ThrowException()
        {
            //arrange
            var movieRatingToPredict = new MovieRating
            {
                UserId = 2,
                MovieId = 2
            };

            //act
            Action action = () => movieRecommender.PredictMovieRating(movieRatingToPredict);

            //assert
            action.Should().Throw<NullReferenceException>();
        }

        [Test]
        public async Task Given_UnpredictableMovie_When_IsPredicted_Then_ItShouldReturnPredictionWithNanAsync()
        {
            //arrange
            var movieRatingToPredict = new MovieRating
            {
                UserId = 3,
                MovieId = 3
            };
            var predictionEngine = await modelBuilder.ConstructMoviePredictModelAsync();
            var movieRecommender = new MovieRecommender(logger, predictionEngine);

            //act
            var moviePrediction = movieRecommender.PredictMovieRating(movieRatingToPredict);

            //arrange
            moviePrediction.Should().NotBeNull();
            moviePrediction.Score.Should().Be(float.NaN);
            moviePrediction.Label.Should().Be(0);
        }
    }
}