using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;
using Watchster.Application.Interfaces;
using Watchster.Application.Utils.ML.Models;

namespace Watchster.Application.UnitTests.Utils.ML
{
    public class PredictMovieTests
    {
        private readonly IMovieRecommender movieRecommender;

        public PredictMovieTests()
        {
            movieRecommender = A.Fake<IMovieRecommender>();
        }

        [Test]
        public void Given_Movie_When_IsPredicted_Should_GiveNotNullPrediction()
        {
            var movieRatingToPredict = new MovieRating
            {
                UserId = 1,
                MovieId = 1,
                Label = 1
            };

            var moviePrediction = movieRecommender.PredictMovieRating(movieRatingToPredict);

            movieRatingToPredict.Should().NotBeNull();
            moviePrediction.Should().NotBeNull();
            moviePrediction.Label.Should().NotBe(null);
            moviePrediction.Score.Should().NotBe(null);
        }

        [Test]
        public void Given_Movie_When_IsPredicted_Should_HaveScoreInSpecificRange()
        {
            var movieRatingToPredict = A.Fake<MovieRating>();

            var moviePrediction = movieRecommender.PredictMovieRating(movieRatingToPredict);

            moviePrediction.Score.Should().BeInRange(0.0f, 10.0f);
        }
    }
}