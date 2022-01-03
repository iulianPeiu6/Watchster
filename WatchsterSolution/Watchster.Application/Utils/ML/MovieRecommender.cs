using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.ML;
using Watchster.Application.Interfaces;
using Watchster.Application.Utils.ML.Models;

namespace Watchster.Application.Utils.ML
{
    public class MovieRecommender : IMovieRecommender
    {
        private readonly ILogger<MovieRecommender> logger;
        private readonly PredictionEngine<MovieRating, MovieRatingPrediction> predictionEngine;

        public MovieRecommender(
            ILogger<MovieRecommender> logger, 
            IMLModelBuilder modelBuilder)
        {
            this.logger = logger;
            this.predictionEngine = modelBuilder.ConstructMoviePredictModelAsync().Result;
        }

        public MovieRatingPrediction PredictMovieRating(MovieRating movie)
        {
            logger.LogInformation($"Predict Movie Rating for Movie: {movie.MovieId}");
            return predictionEngine.Predict(movie);
        }
    }
}
