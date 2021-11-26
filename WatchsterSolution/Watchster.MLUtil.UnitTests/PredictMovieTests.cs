using FakeItEasy;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System;
using Watchster.MLUtil.Models;
using Watchster.MLUtil.Services.Interfaces;
using FluentAssertions;
using Watchster.MLUtil.Services;
using System.IO;

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