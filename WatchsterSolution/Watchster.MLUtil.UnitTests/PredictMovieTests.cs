using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;
using Watchster.MLUtil.Models;
using Watchster.MLUtil.Services.Interfaces;

namespace Watchster.MLUtil.UnitTests
{
    public class PredictMovieTests
    {
        private readonly IMovieRecommender movieRecommender;

        public PredictMovieTests()
        {
            movieRecommender = A.Fake<IMovieRecommender>();
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Given_Movie_When_IsPredicted_Should_GiveNotNullPrediction()
        {
            var movieRatingToPredict = A.Fake<MovieRating>();

            var moviePrediction = movieRecommender.PredictMovieRating(movieRatingToPredict);

            moviePrediction.Should().NotBe(null);
        }

        [Test]
        public void Given_Movie_When_IsPredicted_Should_HaveScoreInSpecificRange()
        {
            var movieRatingToPredict = A.Fake<MovieRating>();

            var moviePrediction = movieRecommender.PredictMovieRating(movieRatingToPredict);

            moviePrediction.Score.Should().BeInRange(0.0f, 5.0f);
        }
    }
}