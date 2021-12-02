using Watchster.MLUtil.Models;

namespace Watchster.MLUtil.Services.Interfaces
{
    public interface IMovieRecommender
    {
        MovieRatingPrediction PredictMovieRating(MovieRating movie);
    }
}