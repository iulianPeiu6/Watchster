using Microsoft.ML;
using System.Threading.Tasks;
using Watchster.Application.Utils.ML.Models;

namespace Watchster.Application.Utils.ML
{
    public interface IMLModelBuilder
    {
        Task<PredictionEngine<MovieRating, MovieRatingPrediction>> ConstructMoviePredictModelAsync();
    }
}