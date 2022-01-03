using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.ML;
using Microsoft.ML.Trainers;
using System.Linq;
using System.Threading.Tasks;
using Watchster.Application.Features.Queries;
using Watchster.Application.Utils.ML.Models;

namespace Watchster.Application.Utils.ML
{
    public class MLModelBuilder : IMLModelBuilder
    {
        private readonly ILogger<MLModelBuilder> logger;
        private readonly IMediator mediator;
        private readonly MLContext mlContext;
        private ITransformer model;
        private IDataView trainingDataView;
        private IDataView testDataView;
        private const double TrainTestRatio = 0.2;

        public MLModelBuilder(ILogger<MLModelBuilder> logger, IMediator mediator)
        {
            this.logger = logger;
            this.mediator = mediator;
            mlContext = new MLContext();
        }

        public async Task<PredictionEngine<MovieRating, MovieRatingPrediction>> ConstructMoviePredictModelAsync()
        {
            (trainingDataView, testDataView) = await LoadDataAsync();
            BuildAndTrainModel(trainingDataView);
            EvaluateModel(testDataView);
            var predictionEngine = mlContext.Model.CreatePredictionEngine<MovieRating, MovieRatingPrediction>(model);
            return predictionEngine;
        }

        private async Task<(IDataView training, IDataView test)> LoadDataAsync()
        {
            var ratings = await mediator.Send(new GetAllRatingsQuery());
            var dataset = ratings.Select(ratings => new MovieRating
            {
                MovieId = ratings.MovieId,
                UserId = ratings.UserId,
                Label = (float)ratings.RatingValue,
            });

            IDataView dataView = mlContext.Data.LoadFromEnumerable<MovieRating>(dataset);

            logger.LogInformation($"Dataset loaded. Spliting Dataset using {1 - TrainTestRatio}:{TrainTestRatio} Train:Test Ratio");

            var dataSplit = mlContext.Data
                .TrainTestSplit(dataView, testFraction: TrainTestRatio);

            return (dataSplit.TrainSet, dataSplit.TestSet);
        }

        private void BuildAndTrainModel(IDataView trainingDataView)
        {
            logger.LogInformation($"Training model ...");
            IEstimator<ITransformer> estimator = mlContext.Transforms.Conversion
                            .MapValueToKey(outputColumnName: "UserIdEncoded", inputColumnName: "UserId")
                            .Append(mlContext.Transforms.Conversion.MapValueToKey(outputColumnName: "MovieIdEncoded", inputColumnName: "MovieId"));

            var options = new MatrixFactorizationTrainer.Options
            {
                MatrixColumnIndexColumnName = "UserIdEncoded",
                MatrixRowIndexColumnName = "MovieIdEncoded",
                LabelColumnName = "Label",
                NumberOfIterations = 20,
                ApproximationRank = 100
            };

            var trainerEstimator = estimator.Append(mlContext.Recommendation().Trainers.MatrixFactorization(options));

            model = trainerEstimator.Fit(trainingDataView);

            logger.LogInformation($"Model trained");
        }

        private void EvaluateModel(IDataView testDataView)
        {
            var prediction = model.Transform(testDataView);
            var metrics = mlContext.Regression.Evaluate(prediction, labelColumnName: "Label", scoreColumnName: "Score");
            logger.LogInformation($"Root Mean Squared Error : {metrics.RootMeanSquaredError}");
            logger.LogInformation($"RSquared: {metrics.RSquared}");
        }
    }
}
