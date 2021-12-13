using Watchster.Application.Utils.ML.Models;

namespace Watchster.Application.Interfaces
{
    public interface IMovieRecommender
    {
        MovieRatingPrediction PredictMovieRating(MovieRating movie);
    }
}