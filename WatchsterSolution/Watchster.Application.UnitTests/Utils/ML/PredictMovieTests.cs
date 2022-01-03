using FakeItEasy;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.ML;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using Watchster.Application.Interfaces;
using Watchster.Application.Utils.ML;
using Watchster.Application.Utils.ML.Models;

namespace Watchster.Application.UnitTests.Utils.ML
{
    public class PredictMovieTests
    {

        private readonly IMovieRecommender movieRecommender;
        private readonly IMovieRecommender fakeMovieRecommender;
        private readonly ILogger<MovieRecommender> logger;
        private readonly IMLModelBuilder modelBuilder;
        private readonly MovieRating validMovieRating;
        //private readonly PredictionEngine<MovieRating, MovieRatingPrediction> predictEngine;

        public PredictMovieTests()
        {
            validMovieRating = new MovieRating
            {
                UserId = 1,
                MovieId = 1,
                Label = 1
            };

            logger = A.Fake<ILogger<MovieRecommender>>();
            modelBuilder = A.Fake<IMLModelBuilder>();
            var fakeMovieRecommenderTmp = new Fake<IMovieRecommender>();
            fakeMovieRecommenderTmp.CallsTo(mr => mr.PredictMovieRating(validMovieRating))
                .Returns(new MovieRatingPrediction { Label = 5, Score = 5 });
            fakeMovieRecommender = fakeMovieRecommenderTmp.FakedObject;
            movieRecommender = new MovieRecommender(logger, modelBuilder);
        }

        [SetUp]
        public void SetUp()
        {
            Fake.ClearRecordedCalls(logger);
            Fake.ClearRecordedCalls(modelBuilder);
        }

        [TearDown]
        public void TearDown()
        {
            Fake.ClearRecordedCalls(logger);
            Fake.ClearRecordedCalls(modelBuilder);
        }

        [Test]
        public void Given_MovieRecommender_When_IsConstructed_Then_ConstructMoviePredictModelAsyncShouldBeCalled()
        {
            //arrange
            
            //act

            //assert
            A.CallTo(() => modelBuilder.ConstructMoviePredictModelAsync());
        }

        [Test]
        public void Given_Movie_When_IsPredictedWithNullPredictEngine_Then_ThrowException()
        {
            //arrange
            var movieRatingToPredict = validMovieRating;

            //act
            Action action = () => movieRecommender.PredictMovieRating(movieRatingToPredict);

            //assert
            action.Should().Throw<NullReferenceException>();
        }

        [Test]
        public void Given_Movie_When_IsPredicted_Then_ItShouldReturnValidPrediction()
        {
            //arrange
            var movieRatingToPredict = validMovieRating;

            //act
            var moviePrediction = fakeMovieRecommender.PredictMovieRating(movieRatingToPredict);

            //arrange
            moviePrediction.Should().NotBeNull();
            moviePrediction.Score.Should().BeInRange(0.0f, 10.0f);
            moviePrediction.Label.Should().BeInRange(0.0f, 10.0f);
        }
    }
}