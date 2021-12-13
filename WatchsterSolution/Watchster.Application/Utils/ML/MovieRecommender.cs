using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.ML;
using Microsoft.ML.Trainers;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Watchster.Application.Features.Queries;
using Watchster.Application.Interfaces;
using Watchster.Application.Utils.ML.Models;

namespace Watchster.Application.Utils.ML
{
    public class MovieRecommender : IMovieRecommender
    {
        private readonly ILogger<MovieRecommender> logger;
        private readonly IMediator mediator;
        private readonly MLContext mlContext;
        private ITransformer model;
        private PredictionEngine<MovieRating, MovieRatingPrediction> predictionEngine;
        private const double TrainTestRatio = 0.2;
        private readonly string modelPath;

        public MovieRecommender(ILogger<MovieRecommender> logger, IMediator mediator)
        {
            this.logger = logger;
            this.mediator = mediator;
            mlContext = new MLContext();
            modelPath = Path.Combine(Environment.CurrentDirectory, 
                "..", 
                "Watchster.Application", 
                "Utils",
                "ML",
                "Data",
                "MovieRecommenderModel.zip");

            if (File.Exists(modelPath))
            {
                logger.LogInformation("Model is already saved locally.");
                LoadModel();
            }
            else
            {
                ConstructMoviePredictModelAsync().Wait();
            }
        }

        public MovieRatingPrediction PredictMovieRating(MovieRating movie)
        {
            logger.LogInformation($"Predict Movie Rating for Movie: {movie.MovieId}");
            return predictionEngine.Predict(movie);
        }

        private void LoadModel()
        {
            logger.LogInformation($"Loading model from '{modelPath}'...");
            DataViewSchema modelSchema;

            model = mlContext.Model.Load(modelPath, out modelSchema);
            predictionEngine = mlContext.Model.CreatePredictionEngine<MovieRating, MovieRatingPrediction>(model);
        }

        private async Task ConstructMoviePredictModelAsync()
        {
            (IDataView trainingDataView, IDataView testDataView) = await LoadDataAsync();
            BuildAndTrainModel(trainingDataView);
            EvaluateModel(testDataView);
            predictionEngine = mlContext.Model.CreatePredictionEngine<MovieRating, MovieRatingPrediction>(model);
            SaveModel(trainingDataView.Schema);
        }

        private async Task<(IDataView training, IDataView test)> LoadDataAsync()
        {
            var ratings = await mediator.Send(new GetAllRatingsQuery());
            var dataset = ratings.Select(ratings => new MovieRating
            {
                MovieId = ratings.MovieId.ToString(),
                UserId = ratings.UserId.ToString(),
                Label = (float)ratings.RatingValue,
            });

            var dataPath = Path.Combine(Environment.CurrentDirectory, "Data", "rating.csv");

            logger.LogInformation($"Loading Dataset from '{dataPath}'...");

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

        private void SaveModel(DataViewSchema schema)
        {
            logger.LogInformation($"Saving the model to file '{modelPath}'...");
            mlContext.Model.Save(model, schema, modelPath);
        }
    }
}
